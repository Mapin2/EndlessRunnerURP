using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    [HideInInspector] public static GameManager Instance = null;

    [HideInInspector] public bool isGameActive = false;

    [SerializeField] TextMeshProUGUI scoreText = null, highScoreText = null;

    [SerializeField] GameObject[] maps = null, characters = null;

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
        StartGame();
    }

    public void StartGame()
    {
        SelectMapAndCharacter();
        isGameActive = true;
    }

    private void SelectMapAndCharacter()
    {
        // Default value
        GameObject mapSelected = maps[0];
        GameObject characterSelected = characters[0];
        // Get name from player prefs
        string mapName = PlayerPrefs.GetString("MapSelected");
        string characterName = PlayerPrefs.GetString("CharacterSelected");
        foreach (GameObject map in maps)
        {
            if (mapName.Equals(map.name))
            {
                mapSelected = map;
                break;
            }
        }
        foreach (GameObject character in characters)
        {
            if (characterName.Equals(character.name))
            {
                characterSelected = character;
                break;
            }
        }
        // Instantiate selected character and map from the static class PersistSelection
        Instantiate(mapSelected, mapSelected.transform.position, Quaternion.identity);
        Instantiate(characterSelected, characterSelected.transform.position, Quaternion.identity);
    }

    public void EndGame()
    {
        isGameActive = false;
        // Update score in player prefs
        if (ScoreManager.Instance.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(ScoreManager.Instance.score));
        }
    }

    public void UpdateScore(float score)
    {
        scoreText.text = "SCORE: " + Mathf.RoundToInt(score);
    }

    public void UpdateHighScore(float highScore)
    {
        highScoreText.text = "HIGH SCORE: " + Mathf.RoundToInt(highScore);
    }
}
