using UnityEngine;

public class InfectionHandler : MonoBehaviour
{
    [Header("Projectile Settings")] [SerializeField]
    private Projectile projectilePrefab;

    [Header("Explosion Settings")] [SerializeField]
    private ExplosionFX explosionPrefab;

    [SerializeField] private float explosionMultiplier = 3f;


    public Projectile SpawnProjectile(Vector3 position, Vector3 direction)
    {
        var projectile = Instantiate(projectilePrefab, position, Quaternion.LookRotation(direction));
        projectile.Initialize(this);
        return projectile;
    }

    public void HandleProjectileHit(Vector3 hitPoint, float projectileScale)
    {
        var explosion = Instantiate(explosionPrefab, hitPoint, Quaternion.identity);
        explosion.Initialize(projectileScale, explosionMultiplier);
    }
}