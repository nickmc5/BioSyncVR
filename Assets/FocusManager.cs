using UnityEngine;
using UnityEngine.UI;

public class FocusManager : MonoBehaviour
{
    public static FocusManager Instance;

    [SerializeField] private Slider focusSlider;
    private float focusLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        focusSlider.onValueChanged.AddListener(UpdateFocusLevel);
    }

    private void UpdateFocusLevel(float value)
    {
        focusLevel = value;
        // You can add any additional logic here that should occur when the focus level changes
    }

    public float GetFocusLevel()
    {
        return focusLevel;
    }
}