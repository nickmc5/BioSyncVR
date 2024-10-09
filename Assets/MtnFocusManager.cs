using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.RainMaker;
using WaterSystem;

public class MtnFocusManager : MonoBehaviour
{
    public static MtnFocusManager Instance;

    [SerializeField] private byte[] receiveBuffer;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Cubemap sunnySkybox;
    [SerializeField] private Cubemap stormySkybox;
    [SerializeField] private float initialFocusLevel = 0.5f;
    [SerializeField] private RainScript rainScript;
    [SerializeField] private float maxRainIntensity = 0.3f;
    [SerializeField] private ParticleSystem windParticles;
    [SerializeField] private Renderer planeRenderer;

    // New fire effect particle systems
    [SerializeField] private ParticleSystem sparksParticles;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private ParticleSystem smokeParticles;

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

    private void SetInitialFocusLevel()
    {
        focusLevel = Mathf.Clamp01(initialFocusLevel);
        UpdateAllEffects();
    }

    private void UpdateFocusLevel(float value)
    {
        focusLevel = value;
        UpdateAllEffects();
    }

    private void UpdateAllEffects()
    {
        UpdateSkyboxBlend();
        UpdateRainIntensity();
        UpdateWindIntensity();
        UpdatePlaneOpacity();
        UpdateFireEffect(); // New method to update fire effect
    }

    private void UpdateSkyboxBlend()
    {
        float blend = 0f;

        if (focusLevel <= 0.3f)
            blend = 0f;
        else if (focusLevel >= 0.7f)
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

    private void UpdatePlaneOpacity()
    {
        if (planeRenderer != null && planeRenderer.material != null)
        {
            Color currentColor = planeRenderer.material.color;
            currentColor.a = focusLevel;
            planeRenderer.material.color = currentColor;

            planeRenderer.material.SetFloat("_Mode", 3);
            planeRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            planeRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            planeRenderer.material.SetInt("_ZWrite", 0);
            planeRenderer.material.DisableKeyword("_ALPHATEST_ON");
            planeRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            planeRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            planeRenderer.material.renderQueue = 3000;
        }
    }

    // New method to update fire effect
    private void UpdateFireEffect()
    {
        if (fireParticles != null && smokeParticles != null && sparksParticles != null)
        {
            // Calculate emission rates based on focus level
            float fireAndSmokeEmissionRate = Mathf.Lerp(1f, 10f, focusLevel);
            float sparksEmissionRate = Mathf.Lerp(1f, 100f, focusLevel);

            // Update fire particles
            var fireEmission = fireParticles.emission;
            fireEmission.rateOverTime = fireAndSmokeEmissionRate;

            // Update smoke particles
            var smokeEmission = smokeParticles.emission;
            smokeEmission.rateOverTime = fireAndSmokeEmissionRate;

            // Update sparks particles
            var sparksEmission = sparksParticles.emission;
            sparksEmission.rateOverTime = sparksEmissionRate;
        }
    }

    public float GetFocusLevel()
    {
        return focusLevel;
    }

    public void SetFocusLevel(float newLevel)
    {
        focusLevel = Mathf.Clamp01(newLevel);
        UpdateAllEffects();
    }
}