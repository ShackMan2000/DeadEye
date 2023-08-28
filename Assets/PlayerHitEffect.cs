using UnityEngine;

public class PlayerHitEffect : MonoBehaviour
{
    public Renderer hitEffectRenderer;
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 1.0f;

    private Color initialColor;
    private Color targetColor;
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float fadeStartTime;

    private void Start()
    {
        initialColor = hitEffectRenderer.material.color;
        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        hitEffectRenderer.material.color = initialColor;
    }

    private void Update()
    {
        if (isFadingIn)
        {
            float timeElapsed = Time.time - fadeStartTime;
            float t = Mathf.Clamp01(timeElapsed / fadeInDuration);

            Color newColor = Color.Lerp(initialColor, targetColor, t);
            hitEffectRenderer.material.color = newColor;

            if (t >= 1f)
            {
                isFadingIn = false;
                isFadingOut = true;
                fadeStartTime = Time.time;
            }
        }
        else if (isFadingOut)
        {
            float timeElapsed = Time.time - fadeStartTime;
            float t = Mathf.Clamp01(timeElapsed / fadeOutDuration);

            Color newColor = Color.Lerp(targetColor, initialColor, t);
            hitEffectRenderer.material.color = newColor;

            if (t >= 1f)
            {
                isFadingOut = false;
            }
        }
    }

    public void TriggerHitEffect()
    {
        isFadingIn = true;
        fadeStartTime = Time.time;
    }
}
