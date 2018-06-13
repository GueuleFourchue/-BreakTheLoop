using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPulseGlow : MonoBehaviour {

    Light light;

    public float pulseTime;
    public float emissionMultiplier = 1.5f;
    public float lightMultiplier = 1.5f;

    float timer;

    Renderer rend;
    Material mat;
 
    Color originEmissiveColor;
    Color endEmissiveColor;
    Color newEmissiveColor;

    float originLightIntensity;
    float endLightIntensity;

    bool isIncreasing = true;

   
    void Start()
    {
        light = GetComponentInChildren<Light>();
        originLightIntensity = light.intensity;

        rend = GetComponent<Renderer>();
        mat = GetComponent<Renderer>().material;
        originEmissiveColor = mat.GetColor("_EmissionColor");
        newEmissiveColor = originEmissiveColor;
    }

    private void Update()
    {
        Glow();  
    }

    void Glow()
    {
        timer += Time.deltaTime / pulseTime;

        if (isIncreasing)
        {
            newEmissiveColor = Color.Lerp(originEmissiveColor, originEmissiveColor * emissionMultiplier, timer);
            mat.SetColor("_EmissionColor", newEmissiveColor);
            light.intensity = Mathf.Lerp(originLightIntensity, originLightIntensity * lightMultiplier, timer);

            if (light.intensity == originLightIntensity * lightMultiplier)
            {
                endEmissiveColor = newEmissiveColor;
                endLightIntensity = light.intensity;
                timer = 0;
                isIncreasing = false;
            }
        }
        else
        {
            newEmissiveColor = Color.Lerp(endEmissiveColor, originEmissiveColor, timer);
            mat.SetColor("_EmissionColor", newEmissiveColor);
            light.intensity = Mathf.Lerp(endLightIntensity, originLightIntensity, timer);

            if (light.intensity == originLightIntensity )
            {
                timer = 0;
                isIncreasing = true;
            }
        }
    }
}
