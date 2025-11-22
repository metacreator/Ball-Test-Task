using UnityEngine;
using DG.Tweening;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flySpeed = 12f;
    [SerializeField] private float minScale = 0.15f;
    [SerializeField] private float maxScale = 0.35f;
    [SerializeField] private float pulseDuration = 0.4f;

    private bool _fly;
    private Tween _pulseTween;

    private void Update()
    {
        if (_fly)
            transform.position += transform.forward * (flySpeed * Time.deltaTime);
    }

    public void StartCharging()
    {
        _fly = false;

        transform.localScale = Vector3.one * minScale;

        transform.DOKill();

        _pulseTween = transform
            .DOScale(maxScale, pulseDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Launch()
    {
        _fly = true;
        transform.DOKill();
    }
}