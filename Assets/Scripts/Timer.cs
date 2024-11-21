using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;

public class Timer : MonoBehaviour
{
    public static float targetTime;
    public bool paused = false;
    public SceneChange sceneChanger;
    public static float currentTime;

    [SerializeField] private GameObject endPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    void Awake()
    {
        targetTime = 60 * (float)SessionSettings.sessionLength;
        currentTime = targetTime;

        // Make sure panel is hidden at start
        if (endPanel != null)
        {
            endPanel.SetActive(false);
        }

        // Set up button listeners
        SetupButtons();
    }

    private void SetupButtons()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(HandleContinueButton);
        }
        else
        {
            Debug.LogWarning("Continue Button reference is missing in Timer script!");
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(HandleMenuButton);
        }
        else
        {
            Debug.LogWarning("Menu Button reference is missing in Timer script!");
        }
    }

    void Update()
    {
        if (!paused)
        {
            if (currentTime <= 0.0f)
            {
                timerEnded();
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

    void timerEnded()
    {
        Debug.Log("Time's up! Waited for " + targetTime + " seconds.");
        paused = true;

        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("End Panel reference is missing in Timer script!");
        }
    }

    // Continue button simply hides the panel
    private void HandleContinueButton()
    {
        endPanel.SetActive(false);
        // Optional: You might want to keep the timer paused
        // or reset/restart it here depending on your needs
    }

    // Menu button changes scenes
    private void HandleMenuButton()
    {
        sceneChanger.LoadScene("MainMenu");
        sceneChanger.UnloadScene("BeachScene");
    }
}