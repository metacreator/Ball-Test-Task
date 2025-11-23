using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float moveDuration = 0.45f;
    [SerializeField] private float bounceHeight = 1f;
    [SerializeField] private float stopBeforeObstacle = 0.6f;
    [SerializeField] private float sphereRadius = 0.25f;

    [Header("References")] [SerializeField]
    private Transform door;

    [SerializeField] private LayerMask obstacleMask;

    public bool IsMoving { get; private set; }

    private void OnEnable()
    {
        ExplosionFX.OnExplosionFinished += TryMoveForward;
    }

    private void OnDisable()
    {
        ExplosionFX.OnExplosionFinished -= TryMoveForward;
    }

    public void StopImmediatelyInFrontOfDoor(Vector3 currentPos)
    {
        IsMoving = false;
        DOTween.Kill(transform);
        transform.position = new Vector3(currentPos.x, transform.position.y, currentPos.z);
    }


    private void TryMoveForward()
    {
        if (IsMoving) return;

        var pos = transform.position;
        var dir = (door.position - pos);
        dir.y = 0f;
        dir.Normalize();

        if (Physics.SphereCast(pos + Vector3.up * 0.5f,
                sphereRadius,
                dir,
                out var hit,
                100f,
                obstacleMask))
        {
            var playerRadius = transform.localScale.x * 0.5f;
            var obstacleRadius = hit.collider.bounds.extents.x;

            var extraOffset = obstacleRadius * 0.5f;

            var d = hit.distance - (playerRadius + stopBeforeObstacle + extraOffset);

            if (d <= 0.1f)
                return;

            var target = pos + dir * d;
            MoveTo(target);
        }
        else
        {
            var target = new Vector3(door.position.x, pos.y, door.position.z);
            MoveTo(target);
        }
    }


    private void MoveTo(Vector3 target)
    {
        IsMoving = true;

        var start = transform.position;
        var baseY = start.y;

        var dist = Vector3.Distance(start, target);

        var jumps =
            dist < 2f ? 1 :
            dist < 4f ? 2 :
            dist < 7f ? 3 :
            4;

        var duration = Mathf.Clamp(dist * 0.25f, 0.3f, 0.9f);

        var jumpPower = bounceHeight;

        transform
            .DOJump(target, jumpPower, jumps, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                var pos = transform.position;
                pos.y = baseY;
                transform.position = pos;

                IsMoving = false;
            });
    }
}