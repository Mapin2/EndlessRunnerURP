using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    [SerializeField] GameObject[] previewMaps = null, previewCharacters = null;
    // Working with linked list for easiest handling (Use of previous and next, first and last)
    LinkedList<GameObject> previewMapsLinked = null, previewCharactersLinked = null;
    LinkedListNode<GameObject> currentMapNode = null, currentCharacterNode = null;

    GameObject currentMapPreview = null, currentCharacterPreview = null;

    void Start()
    {
        // Create linked list with the array values
        previewMapsLinked = new LinkedList<GameObject>(previewMaps);
        previewCharactersLinked = new LinkedList<GameObject>(previewCharacters);

        // Get current nodes from linked list to instantiate
        currentMapNode = previewMapsLinked.Find(previewMaps[Random.Range(0, previewMaps.Length)]);
        currentMapPreview = Instantiate(currentMapNode.Value, currentMapNode.Value.transform.position, Quaternion.identity);

        currentCharacterNode = previewCharactersLinked.Find(previewCharacters[Random.Range(0, previewCharacters.Length)]);
        currentCharacterPreview = Instantiate(currentCharacterNode.Value, currentCharacterNode.Value.transform.position, Quaternion.identity);
    }

    public void RightSelectMap()
    {
        currentMapNode = currentMapNode.Next;
        Destroy(currentMapPreview);
        if (currentMapNode == null)
        {
            currentMapNode = previewMapsLinked.First;
        }
        currentMapPreview = Instantiate(currentMapNode.Value, currentMapNode.Value.transform.position, Quaternion.identity);
        AudioManager.Instance.Play("ConfirmButton");
    }

    public void LeftSelectMap()
    {
        currentMapNode = currentMapNode.Previous;
        Destroy(currentMapPreview);
        if (currentMapNode == null)
        {
            currentMapNode = previewMapsLinked.Last;
        }
        currentMapPreview = Instantiate(currentMapNode.Value, currentMapNode.Value.transform.position, Quaternion.identity);
        AudioManager.Instance.Play("ConfirmButton");
    }
    public void RightSelectCharacter()
    {
        currentCharacterNode = currentCharacterNode.Next;
        Destroy(currentCharacterPreview);
        if (currentCharacterNode == null)
        {
            currentCharacterNode = previewCharactersLinked.First;
        }
        currentCharacterPreview = Instantiate(currentCharacterNode.Value, currentCharacterNode.Value.transform.position, Quaternion.identity);
        AudioManager.Instance.Play("ConfirmButton");
    }

    public void LeftSelectCharacter()
    {
        currentCharacterNode = currentCharacterNode.Previous;
        Destroy(currentCharacterPreview);
        if (currentCharacterNode == null)
        {
            currentCharacterNode = previewCharactersLinked.Last;
        }
        currentCharacterPreview = Instantiate(currentCharacterNode.Value, currentCharacterNode.Value.transform.position, Quaternion.identity);
        AudioManager.Instance.Play("ConfirmButton");
    }

    public void StartGame()
    {
        // TODO: Investigate another way of persisting the selection
        // Save selection for use it on the game scene (Deleting de (Clone) in the name since it's an instance of a prefab)
        PlayerPrefs.SetString("MapSelected", currentMapPreview.name.Replace("(Clone)", ""));
        PlayerPrefs.SetString("CharacterSelected", currentCharacterPreview.name.Replace("(Clone)", ""));

        AudioManager.Instance.Play("ConfirmButton");
        SceneLoaderManager.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        AudioManager.Instance.Play("CancelButton");
        AudioManager.Instance.Stop("GameTheme");
        SceneLoaderManager.Instance.FadeToLevel(0);
        AudioManager.Instance.Play("MenuTheme");
    }
}
