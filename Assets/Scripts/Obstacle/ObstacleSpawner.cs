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

        var start = player.position;
        var end = door.position;
        var forward = (end - start).normalized;
        var right = Vector3.Cross(Vector3.up, forward).normalized;

        var totalDistance = Vector3.Distance(start, end);

        var usableDepth = totalDistance - (playerOffset + doorOffset);

        for (var i = 0; i < obstacleCount; i++)
        {
            var depthOffset = Random.Range(0f, usableDepth) - usableDepth * 0.5f;

            var forwardDist = depthOffset + (playerOffset - (doorOffset * 0.5f));

            var sideOffset = Random.Range(-fieldWidth, fieldWidth);

            var spawnPos =
                ((start + end) * 0.5f) +
                forward * forwardDist +
                right * sideOffset;

            spawnPos.y = yPosition;

            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}