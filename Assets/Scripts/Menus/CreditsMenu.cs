using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public void MainMenu()
    {
        AudioManager.Instance.Play("CancelButton");
        SceneLoaderManager.Instance.FadeToLevel(0);
    }
}
