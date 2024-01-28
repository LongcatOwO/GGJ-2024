using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPosition : MonoBehaviour
{
    //This class serves to position the game object it is on in the middle of the two provided transforms to serve as a "Look At" point for the camera.

    public Transform TransformOne { get { return transformOne; } }
    public Transform TransformTwo { get { return transformTwo; } }

    [Header("Target Transforms")]
    [SerializeField] private Transform transformOne;
    [SerializeField] private Transform transformTwo;    

    private void Update()
    {
        if(transformOne == null || transformTwo == null)
        {
            return;
        }

        Vector3 newPosition = (transformOne.position + transformTwo.position) / 2;

        transform.position = newPosition;

        Vector3 perpendicularVector = Vector3.Cross(transformOne.position - transformTwo.position, Vector3.up);

        transform.rotation = Quaternion.LookRotation(perpendicularVector);
    }

    public void SetFocusTargetTranforms(Transform transformOne, Transform transformTwo)
    {
        this.transformOne = transformOne;

        this.transformTwo = transformTwo;
    }

    public void SetTransformOne(Transform transformOne)
    {
        this.transformOne = transformOne;
    }
    
    public void SetTransformTwo(Transform transformTwo)
    {
        this.transformTwo = transformTwo;
    }
}
