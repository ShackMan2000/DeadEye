using UnityEngine;

public class DimmingEffect : MonoBehaviour
{
    // Reference to the main camera
    private Camera mainCamera;

    // Original fog settings
    public bool originalFogState;
    public Color originalFogColor;
    public float originalFogDensity;

    // Dimmed fog settings
    public bool dimmedFogState = false;
    public Color dimmedFogColor = Color.black; // Adjust as needed
    public float dimmedFogDensity = 0.1f; // Adjust as needed

    private void Start()
    {
        mainCamera = Camera.main;

        // Store original fog settings
        originalFogState = RenderSettings.fog;
        originalFogColor = RenderSettings.fogColor;
        originalFogDensity = RenderSettings.fogDensity;

        //ToggleDimmingEffect();
    }

    

    public void ToggleDimmingEffect()
    {
        dimmedFogState = !dimmedFogState;

        // Apply dimming effect by adjusting fog settings
        if (dimmedFogState)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = dimmedFogColor;
            RenderSettings.fogDensity = dimmedFogDensity;
        }
        else
        {
            // Revert to original fog settings
            RenderSettings.fog = originalFogState;
            RenderSettings.fogColor = originalFogColor;
            RenderSettings.fogDensity = originalFogDensity;
        }
    }
}
