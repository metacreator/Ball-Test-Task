using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerBall playerBall;
    [SerializeField] private Door door;

    [SerializeField] private Transform finishLineTransform;
    [SerializeField] private float doorCompleteDistance = 3f;

    public bool LevelFailed { get; private set; }
    public bool LevelCompleted { get; private set; }
    public bool IsPlaying { get; private set; }
    public bool PlayerIsTooSmall => playerBall.IsTooSmall;

    private bool _ended;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        LevelFailed = false;
        LevelCompleted = false;
        IsPlaying = true;
    }


    public void Fail(string reason = "")
    {
        if (LevelFailed || LevelCompleted) return;
        LevelFailed = true;
        IsPlaying = false;
        Debug.Log("LEVEL FAILED: " + reason);
    }


    public void CompleteLevel()
    {
        if (LevelFailed || LevelCompleted) return;

        LevelCompleted = true;
        IsPlaying = false;

        Debug.Log("LEVEL COMPLETE!");
    }

    public void WinLevel()
    {
        if (_ended) return;
        _ended = true;

        Debug.Log("WIN!");
        // TODO: UI animation or load next scene
    }


    public void CheckDoorReached()
    {
        if (LevelCompleted || LevelFailed) return;

        var dist = Vector3.Distance(playerBall.transform.position, finishLineTransform.position);
        if (dist <= doorCompleteDistance)
        {
            CompleteLevel();
        }
    }
}