using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public Color topColor = Color.blue;
    public Color bottomColor = Color.white;
    public Material skyboxMat;
    // Start is called before the first frame update
    void Start()
    {
        skyboxMat = new Material(Shader.Find("Skybox/Procedural"));
        skyboxMat.SetColor("_SkyTint", topColor);
        skyboxMat.SetColor("_GroundColor", bottomColor);
        RenderSettings.skybox = skyboxMat;
        StartCoroutine(UpdateColorCoroutine());
    }

    void Update()
    {

    }

    IEnumerator UpdateColorCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        topColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        bottomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        skyboxMat.SetColor("_SkyTint", topColor);
        skyboxMat.SetColor("_GroundColor", bottomColor);
    }
}


