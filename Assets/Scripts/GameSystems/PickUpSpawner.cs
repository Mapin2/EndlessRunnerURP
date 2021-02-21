using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] ObstacleSpawner obstacleSpawner = null;

    public List<GameObject> pickUps = null;

    [SerializeField] float spawnRate = 4f, increaseSpawnRate = 0.25f, minSpawnRate = 1.75f;
    [SerializeField] int maxItemsSpawned = 0, targetScoreJump = 100;
    float timeWaitNew = 0f, minOverlapTime = 0.15f, minYSPawnPos = -0.20f, maxYSpawnPos = -1f;
    int randomIndex = 0, arrayLength = 0, itemsSpawned = 0, targetScore = 0;

    bool minSpawnRateReached = false;

    float lastTimeSpawned = 0f;

    void Start()
    {
        timeWaitNew = spawnRate;
        arrayLength = pickUps.Count;
        targetScore = targetScoreJump;
    }

    void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            SpawnPickUp();
        }
    }

    private void SpawnPickUp()
    {
        if (!minSpawnRateReached)
        {
            SpawnRateIncrease();
        }

        if (timeWaitNew <= 0 && (maxItemsSpawned == 0 || maxItemsSpawned > itemsSpawned))
        {
            InstantiatePickUp();

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

    private void InstantiatePickUp()
    {
        // Get obstacle to instantiate and calculate Y spawn position
        randomIndex = Random.Range(0, arrayLength);
        GameObject obstacle = pickUps[randomIndex];
        float yPos = Random.Range(minYSPawnPos, maxYSpawnPos);
        lastTimeSpawned = Time.time;

        // Instantiate if the pickUp spawned will not overlap with the obstacle
        if (lastTimeSpawned - obstacleSpawner.lastTimeSpawned > minOverlapTime)
        {
            Instantiate(obstacle, new Vector2(transform.position.x, yPos), Quaternion.identity);
        }
    }
}
