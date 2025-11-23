using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform door;
    [SerializeField] private GameObject obstaclePrefab;

    [Header("Spawn Settings")] [SerializeField]
    private int obstacleCount = 50;

    [Tooltip("Left/Right spread of obstacles")] [SerializeField]
    private float fieldWidth = 3f;

    [Tooltip("Forward spread between player and door")] [SerializeField]
    private float fieldDepth = 8f;

    [Tooltip("Safe zone around player (no obstacles)")] [SerializeField]
    private float playerOffset = 2f;

    [Tooltip("Safe zone around door (no obstacles)")] [SerializeField]
    private float doorOffset = 2f;

    [SerializeField] private float yPosition = 2f;

    private void Start()
    {
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        if (!player || !door || !obstaclePrefab)
        {
            Debug.LogError("Missing references in ObstacleSpawner");
            return;
        }

        Vector3 start = player.position;
        Vector3 end = door.position;
        Vector3 forward = (end - start).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        float totalDistance = Vector3.Distance(start, end);

        // Reduced depth to avoid spawning too close to player/door
        float usableDepth = totalDistance - (playerOffset + doorOffset);

        for (int i = 0; i < obstacleCount; i++)
        {
            // random depth inside usable zone
            float depthOffset = Random.Range(0f, usableDepth) - usableDepth * 0.5f;

            // shift depth so it starts after playerOffset and before doorOffset
            float forwardDist = depthOffset + (playerOffset - (doorOffset * 0.5f));

            // random side offset
            float sideOffset = Random.Range(-fieldWidth, fieldWidth);

            Vector3 spawnPos =
                ((start + end) * 0.5f) +
                forward * forwardDist +
                right * sideOffset;

            spawnPos.y = yPosition;

            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}