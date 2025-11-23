using UnityEngine;
using DG.Tweening;

public class ExplosionFX : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private SphereCollider sphereCollider;

    public void Initialize(float projectileScale, float multiplier)
    {
        var finalScale = projectileScale * multiplier;

        transform.localScale = Vector3.zero;

        transform.DOScale(finalScale, duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => Destroy(gameObject));
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