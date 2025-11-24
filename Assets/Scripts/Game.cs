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
    public string LastFailReason { get; private set; }

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
        LastFailReason = reason;

        LevelFailed = true;
        IsPlaying = false;

        Debug.Log("LEVEL FAILED: " + reason);
    }

    public void WinLevel()
    {
        if (_ended) return;
        _ended = true;

        LevelCompleted = true;
        IsPlaying = false;

        Debug.Log("LEVEL COMPLETE!");
    }
}