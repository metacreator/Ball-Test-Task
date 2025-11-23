using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float CurrentScale { get; private set; }
    [SerializeField] private float flySpeed = 12f;

    private bool _fly;
    private float _lockedY;

    private void Awake()
    {
        _lockedY = transform.position.y;
    }

    private void Update()
    {
        if (!_fly) return;
        var pos = transform.position;
        pos += transform.forward * (flySpeed * Time.deltaTime);
        pos.y = _lockedY;

        transform.position = pos;
    }


    public void SetScale(float scale)
    {
        CurrentScale = scale;
        transform.localScale = Vector3.one * scale;
    }

    public void Launch()
    {
        _fly = true;
    }
}