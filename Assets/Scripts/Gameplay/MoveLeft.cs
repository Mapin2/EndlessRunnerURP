using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float maxSpeed = 3.5f, increaseSpeedRate = 0.25f;
    [SerializeField] int targetScoreJump = 200;
    public float speed = 1.75f;
    int targetScore = 0;
    bool maxSpeedReached = false;

    void Start()
    {
        targetScore = targetScoreJump;
    }

    void Update()
    {
        if (GameManager.Instance.isGameActive)
        {
            if (!maxSpeedReached)
            {
                SpeedIncrease();
            }
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void SpeedIncrease()
    {
        // For every targetScoreJump increase speed in increaseSpeedRate
        float score = ScoreManager.Instance.score;
        if (score >= targetScore)
        {
            // Update to a new targetScore
            targetScore += targetScoreJump;
            speed += increaseSpeedRate;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
                maxSpeedReached = true;
            }
        }
    }
}
