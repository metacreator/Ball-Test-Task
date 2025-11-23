using UnityEngine;

public class InfectionHandler : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private LayerMask obstacleMask;

    private readonly Collider[] _results = new Collider[64]; 

    public Projectile SpawnProjectile(Vector3 pos, Vector3 dir)
    {
        var proj = Instantiate(projectilePrefab, pos, Quaternion.LookRotation(dir));
        proj.Initialize(this, obstacleMask);
        return proj;
    }

    public void HandleProjectileHit(Vector3 hitPoint, float projectileScale)
    {
        var radius = projectileScale * 3f;
        var count = Physics.OverlapSphereNonAlloc(hitPoint, radius, _results, obstacleMask);

        for (var i = 0; i < count; i++)
        {
            _results[i].GetComponent<Obstacle>()?.Infect();
            _results[i] = null;
        }
    }
}