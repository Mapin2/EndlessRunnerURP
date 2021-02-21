using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    [HideInInspector] public static ScoreManager Instance = null;

    [SerializeField] float pointsPerSecond = 0f;

    [HideInInspector] public float score = 0f, highscore = 0f;

    [SerializeField] TextMeshProUGUI scorePickUpText = null;
    [SerializeField] Animator animatorScorePickUp = null;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        highscore = PlayerPrefs.GetInt("HighScore", 0);
        GameManager.Instance.UpdateHighScore(highscore);
    }

    void Update()
    {
        // Update score while game is active
        if (GameManager.Instance.isGameActive)
        {
            score += pointsPerSecond * Time.deltaTime;
            GameManager.Instance.UpdateScore(score);
        }

        // Update highScore
        if (score > highscore)
        {
            highscore = score;
            GameManager.Instance.UpdateHighScore(highscore);
        }
    }

    public void ScorePickUp(int pickUpValue)
    {
        animatorScorePickUp.SetTrigger("ScorePickUp");
        scorePickUpText.text = "+ " + pickUpValue;
        score += pickUpValue;
    }
}
