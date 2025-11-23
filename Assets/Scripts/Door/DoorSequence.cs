using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DoorSequence : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Animator doorAnimator;

    [SerializeField] private string openAnim = "Take 001";
    [SerializeField] private string closeAnim = "Take 001";

    [SerializeField] private Transform doorExitPoint;
    [SerializeField] private Game game;

    [Header("Timing")] [SerializeField] private float moveThroughDuration = 0.5f;
    [SerializeField] private float afterOpenDelay = 0.3f;
    [SerializeField] private float afterMoveDelay = 0.3f;

    private bool _running;

    public void StartWinSequence(PlayerBall player)
    {
        if (_running) return;
        _running = true;
        StartCoroutine(RunSequence(player));
    }

    private IEnumerator RunSequence(PlayerBall player)
    {
        player.StopPlayerMovement();

        doorAnimator.speed = 1f;
        doorAnimator.Play(openAnim, 0, 0f);

        yield return new WaitForSeconds(GetClipLength(openAnim) + afterOpenDelay);

        Vector3 targetPos = new Vector3(
            doorExitPoint.position.x,
            player.transform.position.y,
            doorExitPoint.position.z
        );

        player.transform
            .DOMove(targetPos, moveThroughDuration)
            .SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(moveThroughDuration + afterMoveDelay);

        game.WinLevel();
    }

    public void CloseDoor()
    {
        doorAnimator.speed = -1f;
        doorAnimator.Play(closeAnim, 0, 1f);
    }


    private float GetClipLength(string clipName)
    {
        foreach (var clip in doorAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }

        return 1f;
    }
}