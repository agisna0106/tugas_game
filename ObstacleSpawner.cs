using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject[] obstaclePrefabs;
    public GameObject predatorPrefab;

    public Transform player;

    public float spawnDistance = 10f;
    public float spawnInterval = 2f;

    public float minY = -4f;
    public float maxY = 4f;

    float timer;

    void Start()
    {
        
    }

    void Update()
    {
        if (player == null) return;

        UpdateDifficulty();

        timer += Time.deltaTime;

        if (timer >= spawnInterval) {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void UpdateDifficulty() 
    {
        if (gameManager == null) return;

        if (gameManager.currentPhase == 1) {
            spawnInterval = 3f;
        } else if (gameManager.currentPhase == 2) {
            spawnInterval = 2f;
        } else if (gameManager.currentPhase == 3) {
            spawnInterval = 1.5f;
        }
    }

    void SpawnObstacle()
    {
        if (player == null) return;

        int spawnCount = Random.Range(1, 3);

        for (int i = 0; i < spawnCount; i++)
        {
            float spawnX = player.position.x + spawnDistance;
            float spawnY;

            do
            {
                spawnY = Random.Range(minY, maxY);
            }
            while (Mathf.Abs(spawnY - player.position.y) < 1.5f);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            GameObject randomObstacle = obstaclePrefabs[
                Random.Range(0, obstaclePrefabs.Length)
            ];

            GameObject obs = Instantiate(
                randomObstacle,
                spawnPosition,
                Quaternion.identity
            );

            float randomScale = Random.Range(0.1f, 0.3f);
            obs.transform.localScale = new Vector3(
                randomScale,
                randomScale,
                1f
            );

            float randomRotation = Random.Range(0f, 360f);

            obs.transform.rotation = Quaternion.Euler(
                0f,
                0f,
                randomRotation
            );
        }

        if (Random.value < 0.3f)
        {
            float spawnX = player.position.x + spawnDistance;
            float spawnY = Random.Range(minY, maxY);

            Instantiate(predatorPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        }
    }
}