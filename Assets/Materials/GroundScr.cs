using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScr : MonoBehaviour
{
    
    [SerializeField] public Color colorA;
    [SerializeField] public Color colorB;
    public float duration = 1f;

    private float t = 0;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / duration;

        Color lerpedColor = Color.Lerp(colorA, colorB, t);

        material.SetColor("_Color", lerpedColor);
        
    }
}
