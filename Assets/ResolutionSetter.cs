using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class ResolutionSetter : MonoBehaviour
{
    [SerializeField] private Vector2Int resolutionToSet;

    private void Awake()
    {
        Screen.SetResolution(resolutionToSet.x, resolutionToSet.y, false);
    }
}
