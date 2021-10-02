using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        VikingManager.instance.AddViking();
        VikingManager.instance.AddViking();
        VikingManager.instance.AddViking();
        SceneManager.LoadScene("Map");
    }
    public void OpenOptions()
    {

    }
    public void QuitGame()
    {
        Debug.Log("You quit boi");
        Application.Quit();
    }
}
