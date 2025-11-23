using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float CurrentScale { get; private set; }
    [SerializeField] private float flySpeed = 12f;
    [SerializeField] private LayerMask obstacleMask;

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


    private void OnTriggerEnter(Collider other)
    {
        if (!_fly) return;

        if ((obstacleMask.value & (1 << other.gameObject.layer)) == 0) return;
        _fly = false;

        var infectionRadius = CurrentScale * 3f;

        InfectionHandler.Instance.ProcessInfection(
            transform.position,
            infectionRadius
        );

        Destroy(gameObject);
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