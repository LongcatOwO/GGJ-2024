using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BillBoardRotationAxis { All, Horizontal, Vertical }
public class BillboardToCamera : MonoBehaviour
{
    //This class billboards its host using the specified rotation behvaiour to the camera.
    [SerializeField] private BillBoardRotationAxis axis;

    private Camera activeCamera;    

    private void Awake()
    {
        activeCamera = Camera.main;
    }

    private void Update()
    {
        Quaternion billboardRotation;

        switch (axis)
        {
            case BillBoardRotationAxis.Horizontal:
                {
                    billboardRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(activeCamera.transform.forward, Vector3.up));
                }
                break;
            case BillBoardRotationAxis.Vertical:
                {
                    billboardRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(activeCamera.transform.forward, Vector3.forward));
                }
                break;
            default:
                {
                    billboardRotation = activeCamera.transform.rotation;
                }
                break;
        }        

        transform.rotation = billboardRotation;
    }
}
