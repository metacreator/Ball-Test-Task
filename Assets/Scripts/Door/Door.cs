using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorSequence sequence;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerBall>(out var ball))
            return;

        sequence.StartWinSequence(ball);
    }
}
