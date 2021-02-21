using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> obstacles = null;

    [SerializeField] float spawnRate = 8f, increaseSpawnRate = 0.25f, minSpawnRate = 0.8f;
    [SerializeField] int maxItemsSpawned = 0, targetScoreJump = 100;
    float timeWaitNew = 0f, minYSPawnPos = -0.60f, maxYSpawnPos = -0.93f;
    int randomIndex = 0, arrayLength = 0, itemsSpawned = 0, targetScore = 0;

    bool minSpawnRateReached = false;

    [HideInInspector] public float lastTimeSpawned = 0f;

    void Start()
    {
        timeWaitNew = spawnRate;
        arrayLength = obstacles.Count;
        targetScore = targetScoreJump;
    }

    void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (!minSpawnRateReached)
        {
            SpawnRateIncrease();
        }

        if (timeWaitNew <= 0 && (maxItemsSpawned == 0 || maxItemsSpawned > itemsSpawned))
        {
            InstantiateObstacle();

            // Update spawn time
            timeWaitNew = spawnRate;

            // Min spawn time reached
            if (timeWaitNew < minSpawnRate)
            {
                spawnRate = minSpawnRate;
                timeWaitNew = spawnRate;
                minSpawnRateReached = true;
            }

            // TODO: If maxItemsSpawned is != 0, decrease itemsSpawned from MoveLeftItem when they're deleted
            if (maxItemsSpawned != 0)
            {
                itemsSpawned++;
            }
        }
        else
        {
            timeWaitNew -= Time.deltaTime;
        }
    }

    private void SpawnRateIncrease()
    {
        // For every targetScoreJump decrease spawnRate in increaseSpawnRate
        float score = ScoreManager.Instance.score;
        if (score >= targetScore)
        {
            // Update to a new targetScore
            targetScore += targetScoreJump;
            spawnRate -= increaseSpawnRate;
        }
    }

    private void InstantiateObstacle()
    {
        // Get obstacle to instantiate and calculate Y spawn position
        randomIndex = Random.Range(0, arrayLength);
        GameObject obstacle = obstacles[randomIndex];
        float yPos = obstacle.transform.position.y;
        if (obstacle.CompareTag("FloatingObstacle"))
        {
            yPos = Random.Range(minYSPawnPos, maxYSpawnPos);
        }

        // Save the time to make use of it in PickUpSpawner and avoid overlaping
        lastTimeSpawned = Time.time;
        Instantiate(obstacle, new Vector2(transform.position.x, yPos), Quaternion.identity);
    }
}
