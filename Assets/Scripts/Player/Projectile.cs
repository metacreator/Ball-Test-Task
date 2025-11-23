using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float flySpeed = 12f;

    private InfectionHandler handler;
    private LayerMask obstacleMask;
    private bool fly;
    private float lockedY;
    public float CurrentScale { get; private set; }

    public void Initialize(InfectionHandler h, LayerMask mask)
    {
        handler = h;
        obstacleMask = mask;
        lockedY = transform.position.y;

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (!fly) return;

        var pos = transform.position;
        pos += transform.forward * (flySpeed * Time.deltaTime);
        pos.y = lockedY;
        transform.position = pos;
    }

    public void SetScale(float s)
    {
        CurrentScale = s;
        transform.localScale = Vector3.one * s;
    }

    public void Launch()
    {
        fly = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((obstacleMask.value & (1 << other.gameObject.layer)) == 0) return;
        fly = false;
        handler.HandleProjectileHit(transform.position, CurrentScale);
        Destroy(gameObject);
    }
}