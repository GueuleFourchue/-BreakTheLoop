using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DynamicOutline : MonoBehaviour {

    [Header("Time Values")]
    public float fadeTime = 0.8f;
    public float pulseTime = 1f;

    [Header("Color Values")]
    public float outlineMaxAlpha = 1f;
    public float baseMaxAlpha = 0.35f;
    public float basePulseMinAlpha = 0.2f;

    bool outlineOnAnim;
    bool outlineOffAnim;
    bool isOutlined;

    [HideInInspector]
    public bool active = false;

    [HideInInspector]
    public GameObject root = null;

    [HideInInspector]
    public Material material = null;

    Color color;
    Color baseColor;

    float t;
    float tPulse;
   
    bool increasingPulse;

    Material outlineMat;


    private void Start()
    {
        outlineMat = this.root.GetComponent<Renderer>().material;
        color = outlineMat.GetColor("_OutlineColor");
        baseColor = outlineMat.GetColor("_BaseColor");
    }

    public void ShowOutline(bool show)
    {
        if (show)
        {
            this.root.SetActive(show);
            t = 0;
            color.a = 0;
            baseColor.a = 0;
            outlineMat.SetColor("_OutlineColor", color);
            outlineMat.SetColor("_BaseColor", baseColor);

            outlineOffAnim = false;
            outlineOnAnim = true;
        }
        else
        {
            t = 0;
            color.a = outlineMaxAlpha;
            baseColor.a = baseMaxAlpha;
            outlineMat.SetColor("_OutlineColor", color);
            outlineMat.SetColor("_BaseColor", baseColor);

            isOutlined = false;
            outlineOnAnim = false;
            outlineOffAnim = true;
        }
    }

    public void Update()
    {
        if (outlineOnAnim)
        {
            OutlineFadeIn();
        }
        if (outlineOffAnim)
        {
            OutlineFadeOut();
        }
        
        if (isOutlined)
        {
            OutlinePulse();
        }
    }

    void OutlineFadeIn()
    {
        t += Time.deltaTime / fadeTime;

        //Increase Outline value
        color = Color.Lerp(color, new Color(color.r, color.g, color.b, outlineMaxAlpha), t);
        outlineMat.SetColor("_OutlineColor", color);

        //Increase base tint value
        baseColor = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, baseMaxAlpha), t);
        outlineMat.SetColor("_BaseColor", baseColor);

        if (color.a == outlineMaxAlpha)
        {
            outlineOnAnim = false;
            isOutlined = true;
            increasingPulse = false;
        }
    }
    void OutlineFadeOut()
    {
        t += Time.deltaTime / fadeTime;
        
        //Increase Outline value
        color = Color.Lerp(color, new Color(color.r, color.g, color.b, 0), t);
        outlineMat.SetColor("_OutlineColor", color);

        //Increase base tint value
        baseColor = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, 0), t);
        outlineMat.SetColor("_BaseColor", baseColor);

        if (color.a == 0)
        {
            outlineOffAnim = false;
            this.root.SetActive(false);
        }
    }

    
    void OutlinePulse()
    {
        tPulse += Time.deltaTime / pulseTime;

        if (increasingPulse)
        {
            baseColor = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, baseMaxAlpha), tPulse);
            if (baseColor.a == baseMaxAlpha)
            {
                tPulse = 0;
                increasingPulse = false;
            }
        }
        if (!increasingPulse)
        {
            baseColor = Color.Lerp(baseColor, new Color(baseColor.r, baseColor.g, baseColor.b, basePulseMinAlpha), tPulse);
            if (baseColor.a == basePulseMinAlpha)
            {
                tPulse = 0;
                increasingPulse = true;
            }
        }
        outlineMat.SetColor("_BaseColor", baseColor);
    }
    
}
