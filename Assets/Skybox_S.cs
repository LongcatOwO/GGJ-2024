using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox_S : MonoBehaviour
{
    public Color topColor = Color.blue;
    public Color bottomColor = Color.white;
    public Material skyboxMat;


    public struct ColorStructRGB
    {
        public float R;
        public float G;
        public float B;
        public void RandColors()
        {
            R = Random.Range(0f, 1f);
            G = Random.Range(0f, 1f);
            B = Random.Range(0f, 1f);
        }

        public static bool operator ==(ColorStructRGB a, ColorStructRGB b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B; 
        }

        public static bool operator !=(ColorStructRGB a, ColorStructRGB b)
        {
            return a.R != b.R && a.G != b.G && a.B != b.B;
        }

    }

    private ColorStructRGB TopColorCurrent;
    private ColorStructRGB TopColorNext;
    private float TopColorBlend = 0f;

    private ColorStructRGB BottomColorCurrent;
    private ColorStructRGB BottomColorNext;
    private float BottomColorBlend = 0f;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMat = new Material(Shader.Find("Skybox/Procedural"));
        skyboxMat.SetColor("_SkyTint", topColor);
        skyboxMat.SetColor("_GroundColor", bottomColor);
        RenderSettings.skybox = skyboxMat;

        TopColorCurrent.RandColors();
        BottomColorCurrent.RandColors();

        topColor = new Color(TopColorCurrent.R, TopColorCurrent.G, TopColorCurrent.B);
        bottomColor = new Color(BottomColorCurrent.R, BottomColorCurrent.G, BottomColorCurrent.B);
        skyboxMat.SetColor("_SkyTint", topColor);
        skyboxMat.SetColor("_GroundColor", bottomColor);

        TopColorNext.RandColors();
        BottomColorNext.RandColors();
    }

    void FixedUpdate()
    {

        if (TopColorCurrent != TopColorNext)
        {
            TopColorBlend += 0.01f;
            TopColorCurrent = MatchColors(TopColorCurrent, TopColorNext, TopColorBlend);
        }
        else
        {
            TopColorBlend = 0f;
            TopColorNext.RandColors();
        }

        if (BottomColorCurrent != BottomColorNext)
        {
            BottomColorBlend += 0.01f;
            BottomColorCurrent = MatchColors(BottomColorCurrent, BottomColorNext, BottomColorBlend);
        }
        else
        {
            BottomColorBlend = 0f;
            BottomColorNext.RandColors();
        }

        topColor = new Color(TopColorCurrent.R, TopColorCurrent.G, TopColorCurrent.B);
        bottomColor = new Color(BottomColorCurrent.R, BottomColorCurrent.G, BottomColorCurrent.B);
        skyboxMat.SetColor("_SkyTint", topColor);
        skyboxMat.SetColor("_GroundColor", bottomColor);

    }

    ColorStructRGB MatchColors(ColorStructRGB current, ColorStructRGB next, float blend)
    {
        ColorStructRGB newColor;
        newColor.R = Mathf.Lerp(current.R, next.R, Mathf.Clamp01(blend));
        newColor.G = Mathf.Lerp(current.G, next.G, Mathf.Clamp01(blend));
        newColor.B = Mathf.Lerp(current.B, next.B, Mathf.Clamp01(blend));

        return newColor;

    }

}


