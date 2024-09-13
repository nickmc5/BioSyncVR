using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.RainMaker;

public class FocusManager : MonoBehaviour
{
    public static FocusManager Instance;

    [SerializeField] private Slider focusSlider;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Cubemap sunnySkybox;
    [SerializeField] private Cubemap stormySkybox;
    [SerializeField] private float initialFocusLevel = 0.5f;
    [SerializeField] private RainScript rainScript;
    [SerializeField] private float maxRainIntensity = 0.3f;
    [SerializeField] private ParticleSystem windParticles; // Reference to the wind particle system

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
        skyboxMaterial.SetTexture("_SkyboxSunny", sunnySkybox);
        skyboxMaterial.SetTexture("_SkyboxStormy", stormySkybox);
        RenderSettings.skybox = skyboxMaterial;

        SetInitialFocusLevel();
        focusSlider.onValueChanged.AddListener(UpdateFocusLevel);
    }

    private void SetInitialFocusLevel()
    {
        focusLevel = Mathf.Clamp01(initialFocusLevel);
        if (focusSlider != null)
        {
            focusSlider.value = focusLevel;
        }
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
    }

    private void UpdateFocusLevel(float value)
    {
        focusLevel = value;
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
    }

    private void UpdateSkyboxBlend()
    {
        float blend = 0f;

        if (focusLevel <= 0.6f)
            blend = 0f;
        else if (focusLevel >= 0.8f)
            blend = 1f;
        else
            blend = (focusLevel - 0.6f) / 0.2f;

        skyboxMaterial.SetFloat("_Blend", blend);
        DynamicGI.UpdateEnvironment();
    }

    private void UpdateRainIntensity()
    {
        if (rainScript != null)
        {
            float rainIntensity = 0f;
            if (focusLevel > 0.6f)
            {
                rainIntensity = Mathf.Lerp(0f, maxRainIntensity, (focusLevel - 0.6f) / 0.4f);
            }

            rainScript.RainIntensity = rainIntensity;
        }
    }

    private void UpdateWindIntensity()
    {
        if (windParticles != null)
        {
            float windEmissionRate = Mathf.Lerp(0.2f, 2.0f, focusLevel);
            var emission = windParticles.emission;
            emission.rateOverTime = windEmissionRate;
        }
    }

    public float GetFocusLevel()
    {
        return focusLevel;
    }

    public void SetFocusLevel(float newLevel)
    {
        focusLevel = Mathf.Clamp01(newLevel);
        if (focusSlider != null)
        {
            focusSlider.value = focusLevel;
        }
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
    }
}