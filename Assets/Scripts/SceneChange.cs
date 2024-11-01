using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string currentScene;
    public bool IsImage1Selected = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        currentScene = "MainMenu";
    }

    // public void Load()
    // {
    //     if (IsImage1Selected)
    //     {
    //         LoadScene("BeachScene");
    //         currentScene = "BeachScene";
    //     }
    //     else
    //     {
    //         LoadScene("MountainScene");
    //         currentScene = "MountainScene";
    //     }
    // }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        currentScene = sceneName;
        Debug.Log("Loading scene: " + sceneName);
        if (sceneName == "BeachScene" || sceneName == "MountainScene")
        {
            Debug.Log(SessionSettings.sessionLength);
        }
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadScene(sceneName);
        currentScene = "None";
        Debug.Log("Unloading scene: " + sceneName);
    }
}
