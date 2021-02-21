using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    [HideInInspector] public static SceneLoaderManager Instance = null;
    [SerializeField] Animator fadeAnimator = null;

    private int levelToLoad = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void FadeToLevel(int sceneId)
    {
        levelToLoad = sceneId;
        fadeAnimator.SetTrigger("Fade_Out_Trigger");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
