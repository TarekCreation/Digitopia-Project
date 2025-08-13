using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BackgroundLightColor : MonoBehaviour
{
    public Color SpriteColor = Color.white;
    public Color lightColor = Color.white;
    private float LightIntensity = 1.0f;
    public float transitionDuration = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        LightIntensity = GetComponentInChildren<Light2D>().intensity;

    }
    void OnEnable()
    {
        
        GetComponent<SpriteRenderer>().color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, 0f);
        GetComponentInChildren<Light2D>().color = new Color(lightColor.r, lightColor.g, lightColor.b, 1);
        GetComponentInChildren<Light2D>().intensity = 0f;
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float duration = transitionDuration;

        while (elapsedTime < duration)
        {
            float spriteAlpha = Mathf.Lerp(0f, SpriteColor.a, elapsedTime / duration);
            
            GetComponent<SpriteRenderer>().color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, spriteAlpha);
            GetComponentInChildren<Light2D>().color = new Color(lightColor.r, lightColor.g, lightColor.b, 1f);
            GetComponentInChildren<Light2D>().intensity = Mathf.Lerp(0f, LightIntensity, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = SpriteColor;
        GetComponentInChildren<Light2D>().color = lightColor;
        GetComponentInChildren<Light2D>().intensity = LightIntensity;
    }
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float duration = transitionDuration;

        while (elapsedTime < duration)
        {
            float spriteAlpha = Mathf.Lerp(SpriteColor.a, 0f, elapsedTime / duration);
            
            GetComponent<SpriteRenderer>().color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, spriteAlpha);
            GetComponentInChildren<Light2D>().color = new Color(lightColor.r, lightColor.g, lightColor.b, 1);
            GetComponentInChildren<Light2D>().intensity = Mathf.Lerp(LightIntensity, 0f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        GetComponent<SpriteRenderer>().color = new Color(SpriteColor.r, SpriteColor.g, SpriteColor.b, 0f);
        GetComponentInChildren<Light2D>().color = new Color(lightColor.r, lightColor.g, lightColor.b, 0f);
        GetComponentInChildren<Light2D>().intensity = 0f;
    }

    // Update is called once per frame
    public void PointReachedDestination()
    {
        StartCoroutine(FadeOut());
    }
}
