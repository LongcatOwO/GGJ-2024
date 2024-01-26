using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform _leftFighter;

    [SerializeField]
    Transform _rightFighter;

    float _distanceFromMidPoint = 10;

    void Awake()
    {
        if (_leftFighter == null || _rightFighter == null)
        {
            throw new System.Exception("Fighters need to be assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midPoint = (_leftFighter.position + _rightFighter.position) / 2;
        Vector3 fighterPosDif = _rightFighter.position - _leftFighter.position;
        Vector3 fighterAlignment = fighterPosDif.normalized;
        fighterAlignment = Quaternion.AngleAxis(-90, Vector3.up) * fighterAlignment;
        transform.position = midPoint + fighterAlignment * _distanceFromMidPoint;
        transform.LookAt(midPoint);
    }
}
