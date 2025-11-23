using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Animator animator;

    [SerializeField] private Game game;

    [Header("Animation")] [SerializeField] private string animationName = "Take 001";

    private bool _activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_activated) return;

        if (!other.TryGetComponent<PlayerBall>(out var ball))
            return;

        _activated = true;

        ball.StopPlayerMovement();

        animator.Play(animationName, 0, 0f);

        Debug.Log("Door opened!");

        game.WinLevel();
    }
}