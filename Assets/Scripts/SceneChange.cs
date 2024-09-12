using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
        if (sceneName == "BeachScene")
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
