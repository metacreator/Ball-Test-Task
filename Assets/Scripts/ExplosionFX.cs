using UnityEngine;
using System;
using DG.Tweening;

public class ExplosionFX : MonoBehaviour
{
    public static event Action OnExplosionFinished;

    [SerializeField] private float duration = 0.25f;
    [SerializeField] private SphereCollider sphereCollider;

    private float _finalScale;

    public void Initialize(float projectileScale, float multiplier)
    {
        _finalScale = projectileScale * multiplier;
        transform.localScale = Vector3.zero;

        transform.DOScale(_finalScale, duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                OnExplosionFinished?.Invoke();

                Destroy(gameObject);
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        var obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.Infect();
        }
    }
}