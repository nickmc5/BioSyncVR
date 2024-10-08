using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public bool IsImage1Selected = true;

    public void Load()
    {
        if (IsImage1Selected)
        {
            LoadScene("BeachScene");
        }
        else
        {
            LoadScene("MountainScene");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
        if (sceneName == "BeachScene" || sceneName == "MountainScene")
        {
            Debug.Log(SessionSettings.sessionLength);
        }
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadScene(sceneName);
        Debug.Log("Unloading scene: " + sceneName);
    }
}
