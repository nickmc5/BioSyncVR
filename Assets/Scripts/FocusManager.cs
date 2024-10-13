using UnityEngine;
using DigitalRuby.RainMaker;
using WaterSystem;

public class FocusManager : MonoBehaviour
{
    public static FocusManager Instance;

    //[SerializeField] private byte[] receiveBuffer;
    //[SerializeField] private ServerListener serverScript;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Cubemap sunnySkybox;
    [SerializeField] private Cubemap stormySkybox;
    [SerializeField] private float initialFocusLevel = 0.5f;
    [SerializeField] private RainScript rainScript;
    [SerializeField] private float maxRainIntensity = 0.3f;
    [SerializeField] private ParticleSystem windParticles; // Reference to the wind particle system
    [SerializeField] private Water waterScript;

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
        
    }
	private void Update()
    {
        // Check for changes in the receiveBuffer
        if (MyListener.output != null && MyListener.output > 0)
        {
            float newFocusLevel = MyListener.output;
            if (newFocusLevel != focusLevel)
            {
                UpdateFocusLevel(newFocusLevel);
            }
        }
    }

    private void SetInitialFocusLevel()
    {
        focusLevel = Mathf.Clamp01(initialFocusLevel);
        //if (receiveBuffer != null && receiveBuffer.Length > 0)
        {
           // receiveBuffer[0] = (byte)(focusLevel * 255f);
        }
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
        UpdateWaveHeight();
    }

    private void UpdateFocusLevel(float value)
    {
        focusLevel = value;
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
        UpdateWaveHeight();
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
    private void UpdateWaveHeight()
    {
        if (waterScript != null && waterScript.surfaceData != null)
        {
            float newSwellHeight = Mathf.Lerp(0.1f, 1f, focusLevel);

            // Check if we're using automatic or custom waves
            if (!waterScript.surfaceData._customWaves)
            {
                // Automatic waves
                if (waterScript.surfaceData._basicWaveSettings != null)
                {
                    waterScript.surfaceData._basicWaveSettings.amplitude = newSwellHeight;
                }
            }
            /*else
            {
                // Custom waves
                if (waterScript.surfaceData._waves != null && waterScript.surfaceData._waves.Count > 0)
                {
                    // Update all custom waves
                    for (int i = 0; i < waterScript.surfaceData._waves.Count; i++)
                    {
                        var wave = waterScript.surfaceData._waves[i];
                        wave.amplitude = newSwellHeight;
                        waterScript.surfaceData._waves[i] = wave;
                    }
                }
            }*/

            // After changing the values, we need to reinitialize the waves
            waterScript.Init();
        }
    }

    public float GetFocusLevel()
    {
        return focusLevel;
    }

    public void SetFocusLevel(float newLevel)
    {
        focusLevel = Mathf.Clamp01(newLevel);
        //if (receiveBuffer != null && receiveBuffer.Length > 0)
        {
           // receiveBuffer[0] = (byte)(focusLevel * 255f);
        }
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
        UpdateWaveHeight();
    }
}