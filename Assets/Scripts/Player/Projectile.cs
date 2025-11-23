using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flySpeed = 12f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private Rigidbody _rigidbody;

    private InfectionHandler handler;

    private float lockedY;
    private float timer;
    private bool flying;

    public float CurrentScale { get; private set; }

    public void Initialize(InfectionHandler handler)
    {
        this.handler = handler;

        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;

        lockedY = transform.position.y;
    }

    public void SetScale(float scale)
    {
        CurrentScale = scale;
        transform.localScale = Vector3.one * scale;
    }

    public void Launch()
    {
        flying = true;
    }

    private void Update()
    {
        if (!flying) return;

        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        var pos = transform.position;
        pos += transform.forward * (flySpeed * Time.deltaTime);
        pos.y = lockedY;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() == null) return;
        flying = false;

        handler.HandleProjectileHit(transform.position, CurrentScale);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Obstacle>() == null) return;
        flying = false;

        handler.HandleProjectileHit(transform.position, CurrentScale);
        Destroy(gameObject);
    }
}