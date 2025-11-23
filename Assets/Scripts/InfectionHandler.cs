using UnityEngine;

public class InfectionHandler : MonoBehaviour
{
    public static InfectionHandler Instance { get; private set; }

    [SerializeField] private LayerMask obstacleMask; // set to Obstacle layer only

    private void Awake()
    {
        Instance = this;
    }

    public void ProcessInfection(Vector3 center, float radius)
    {
        // Only obstacles respond because only they are on this layer
        var hits = Physics.OverlapSphere(center, radius, obstacleMask);

        foreach (var hit in hits)
        {
            hit.GetComponent<Obstacle>()?.Infect();
        }
    }
}