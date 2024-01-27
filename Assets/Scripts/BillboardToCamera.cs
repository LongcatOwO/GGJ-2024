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
                    Vector3 horizontalFacingDirection = Vector3.ProjectOnPlane(billboardDirection, Vector3.up);

                    billboardRotation = Quaternion.LookRotation(horizontalFacingDirection);
                }
                break;
            case BillboardRotationAxis.Vertical:
                {
                    Vector3 verticalFacingDirection = Vector3.ProjectOnPlane(billboardDirection, Vector3.forward);

                    billboardRotation = Quaternion.LookRotation(verticalFacingDirection);
                }
                break;
            default:
                {
                    if (inverseFacingDirection)
                    {
                        Vector3 facingDirection = Vector3.ProjectOnPlane(billboardDirection, Vector3.up) + Vector3.ProjectOnPlane(billboardDirection, Vector3.forward);

                        billboardRotation = Quaternion.LookRotation(facingDirection);
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
