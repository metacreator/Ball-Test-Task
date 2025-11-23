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

    private bool _isMoving;
    public bool IsMoving => _isMoving;

    private void OnEnable()
    {
        ExplosionFX.OnExplosionFinished += TryMoveForward;
    }

    private void OnDisable()
    {
        ExplosionFX.OnExplosionFinished -= TryMoveForward;
    }

    private void TryMoveForward()
    {
        if (_isMoving) return;

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
            var d = hit.distance - (playerRadius + stopBeforeObstacle);

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
        _isMoving = true;

        var baseY = transform.position.y;

        var seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(baseY + bounceHeight, moveDuration * 0.5f)
            .SetEase(Ease.OutQuad));

        seq.Join(transform.DOMoveX(target.x, moveDuration).SetEase(Ease.InOutSine));
        seq.Join(transform.DOMoveZ(target.z, moveDuration).SetEase(Ease.InOutSine));

        seq.Append(transform.DOMoveY(baseY, moveDuration * 0.5f)
            .SetEase(Ease.InQuad));

        seq.OnComplete(() => _isMoving = false);
    }
}