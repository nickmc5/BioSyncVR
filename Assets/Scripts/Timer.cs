using UnityEngine;
using System.Collections;

public class Timer: MonoBehaviour {
    // countdown start in seconds
    public static float targetTime; 
    public bool paused = false;
    public SceneChange sceneChanger;
    public static float currentTime;

    void Awake() 
    {
        targetTime = 60 * (float) SessionSettings.sessionLength;
        currentTime = targetTime; 
    }
    void Update() {
        if (!paused) {
            if (currentTime <= 0.0f) {
                timerEnded();
            } else {
                currentTime -= Time.deltaTime;
            }
        }

        //Debug.Log(currentTime);
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
