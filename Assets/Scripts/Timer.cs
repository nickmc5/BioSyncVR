using UnityEngine;
using System.Collections;

public class Timer: MonoBehaviour {
    // countdown start in seconds
    public static float targetTime = 60 * (float) SessionSettings.sessionLength; 
    public bool paused = false;
    public SceneChange sceneChanger;
    private float currentTime = targetTime;

    void Update() {
        if (!paused) {
            if (currentTime <= 0.0f) {
                timerEnded();
            } else {
                currentTime -= Time.deltaTime;
            }
        }
    }

    void timerEnded()
    {
        Debug.Log("Time's up! Waited for " + targetTime + " seconds.");
        paused = true;
        // TODO: change this to load a summary screen instead of main menu
        sceneChanger.LoadScene("MainMenu");
        sceneChanger.UnloadScene("BeachScene");
    }
}
