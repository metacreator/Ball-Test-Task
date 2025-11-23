using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    [Header("VFX")] [SerializeField] private GameObject explosionVFX;

    [Header("Animation Settings")] [SerializeField]
    private Color infectedColor = new Color(1f, 0.5f, 0f);

    [SerializeField] private float colorDelay = 0.25f;
    [SerializeField] private float shrinkDuration = 0.3f;
    [SerializeField] private float destroyDelay = 1.2f;

    private bool _infected;
    private Material _material;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;

        var renderer = GetComponent<Renderer>();
        if (renderer != null)
            _material = renderer.material;
    }

    public void Infect()
    {
        if (_infected) return;
        _infected = true;

        if (_material != null)
            _material.DOColor(infectedColor, 0.15f);

        DOVirtual.DelayedCall(colorDelay, () =>
        {
            var shrinkTarget = new Vector3(0f, _originalScale.y * 0.3f, 0f);

            transform
                .DOScale(shrinkTarget, shrinkDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    GameObject fx = null;
                    if (explosionVFX != null)
                        fx = Instantiate(explosionVFX, transform.position, Quaternion.identity);

                    DOVirtual.DelayedCall(destroyDelay, () =>
                    {
                        if (fx != null)
                            Destroy(fx);

                        Destroy(gameObject);
                    });
                });
        });
    }
}