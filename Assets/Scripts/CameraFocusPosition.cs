using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPosition : MonoBehaviour
{
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

        transform.rotation = Quaternion.LookRotation(perpendicularVector);
    }
}
