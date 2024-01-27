using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPosition : MonoBehaviour
{
    //This class serves to position the game object it is on in the middle of the two provided transforms to serve as a "Look At" point for the camera.

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

        Vector3 perpendicularVector = Vector3.Cross(newPosition, Vector3.up);

      //  Debug.Log(perpendicularVector);

        transform.rotation = Quaternion.LookRotation(perpendicularVector);
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 newPosition = (transformOne.position + transformTwo.position) / 2;

    //    Gizmos.color = Color.red;

    //    Gizmos.DrawSphere(newPosition, 0.1f);

    //    Vector3 perpendicularVector = Vector3.Cross(newPosition, Vector3.up);

    //    Gizmos.color = Color.green;

    //    Gizmos.DrawSphere(newPosition + perpendicularVector, 0.1f);
    //}
}
