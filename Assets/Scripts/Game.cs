using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private PlayerBall playerBall;

    [SerializeField] private Transform door;
    [SerializeField] private float doorCompleteDistance = 3f;

    public bool LevelFailed { get; private set; }
    public bool LevelCompleted { get; private set; }
    public bool IsPlaying { get; private set; }

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


    public void CheckDoorReached()
    {
        if (LevelCompleted || LevelFailed) return;

        var dist = Vector3.Distance(playerBall.transform.position, door.position);
        if (dist <= doorCompleteDistance)
        {
            CompleteLevel();
        }
    }
}