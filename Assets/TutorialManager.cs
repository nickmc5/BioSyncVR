using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Application = UnityEngine.Application;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private Button showTutorialButton;
    [SerializeField] private Button closeTutorialButton;

    private const string TUTORIAL_SHOWN_KEY = "TutorialShown";

    private void Awake()
    {
        // Setup button listeners early, before any canvas state changes
        SetupButtonListeners();
    }

    private void Start()
    {
        // Check if this is first boot
        bool tutorialPreviouslyShown = PlayerPrefs.GetInt(TUTORIAL_SHOWN_KEY, 0) == 1;

        if (!tutorialPreviouslyShown)
        {
            ShowTutorial();
            // Mark tutorial as shown
            PlayerPrefs.SetInt(TUTORIAL_SHOWN_KEY, 1);
            PlayerPrefs.Save();
        }
        else
        {
            HideTutorialCanvas();
        }
    }

    private void SetupButtonListeners()
    {
        if (showTutorialButton != null)
        {
            // Remove any existing listeners first to prevent duplicates
            showTutorialButton.onClick.RemoveAllListeners();
            showTutorialButton.onClick.AddListener(ShowTutorial);
        }
        else
        {
            Debug.LogWarning("Show Tutorial Button not assigned in TutorialManager");
        }

        if (closeTutorialButton != null)
        {
            // Remove any existing listeners first to prevent duplicates
            closeTutorialButton.onClick.RemoveAllListeners();
            closeTutorialButton.onClick.AddListener(CloseTutorial);
        }
        else
        {
            Debug.LogWarning("Close Tutorial Button not assigned in TutorialManager");
        }
    }

    public void ShowTutorial()
    {
        ShowTutorialCanvas();
    }

    public void CloseTutorial()
    {
        HideTutorialCanvas();
    }

    private void ShowTutorialCanvas()
    {
        // Ensure buttons are properly set up before showing
        SetupButtonListeners();
        tutorialCanvas.gameObject.SetActive(true);
    }

    private void HideTutorialCanvas()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }

    public void ResetTutorialState()
    {
        PlayerPrefs.DeleteKey(TUTORIAL_SHOWN_KEY);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        // Ensure listeners are set up if the component gets enabled
        SetupButtonListeners();
    }
}