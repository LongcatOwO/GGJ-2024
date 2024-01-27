using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BillboardRotationAxis { All, Horizontal, Vertical }
public class BillboardToCamera : MonoBehaviour
{
    //This class billboards its host using the specified rotation behvaiour to the camera.
    [Header("Billboard Properties")]
    [SerializeField] private BillboardRotationAxis axis;
    [SerializeField] private bool inverseFacingDirection;

    private Camera activeCamera;    

    private void Awake()
    {
        activeCamera = Camera.main;
    }

    private void Update()
    {
        Quaternion billboardRotation;

        Vector3 billboardDirection = activeCamera.transform.forward;

        if(inverseFacingDirection )
        {
            billboardDirection *= -1;
        }

        switch (axis)
        {
            case BillboardRotationAxis.Horizontal:
                {
                    billboardRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(billboardDirection, Vector3.up));
                }
                break;
            case BillboardRotationAxis.Vertical:
                {
                    billboardRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(billboardDirection, Vector3.forward));
                }
                break;
            default:
                {
                    if (inverseFacingDirection)
                    {
                        billboardRotation = Quaternion.Euler(-activeCamera.transform.rotation.eulerAngles);
                    }
                    else
                    {
                        billboardRotation = activeCamera.transform.rotation;
                    }                    
                }
                break;
        }        

        transform.rotation = billboardRotation;
    }
}
