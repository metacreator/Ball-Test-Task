using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float CurrentScale { get; private set; }
    [SerializeField] private float flySpeed = 12f;

    private bool _fly;

    private void Update()
    {
        if (_fly)
            transform.position += transform.forward * (flySpeed * Time.deltaTime);
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