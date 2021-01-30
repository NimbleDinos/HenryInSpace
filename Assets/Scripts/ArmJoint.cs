using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmJoint : MonoBehaviour {
    public Vector3 RotationAxis;
    public Vector3 StartOffset;
    private Transform _transform;
    public char _rotationAxis;

    private void Awake() {

        if (RotationAxis.x == 1)
        {
            _rotationAxis = 'x';
        }
        else if (RotationAxis.y == 1)
        {
            _rotationAxis = 'y';
        }

        else if (RotationAxis.z == 1)
        {
            _rotationAxis = 'z';    
        }

        _transform = this.transform;
        StartOffset = _transform.localPosition;
    }
}