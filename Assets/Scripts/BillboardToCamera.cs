using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    private Camera activeCamera;

    private void Awake()
    {
        activeCamera = Camera.main;
    }

    private void Update()
    {
        Quaternion billboardRotation = activeCamera.transform.rotation;

        transform.rotation = billboardRotation;

        return;
    }
}
