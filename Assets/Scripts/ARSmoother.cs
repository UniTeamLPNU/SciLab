using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARSmoother : MonoBehaviour
{
    public float positionRatio = 0.1f;
    public float rotationRatio = 0.1f;

    private Vector3 previousPosition;
    private Quaternion previousQuaternion;

    void LateUpdate()
    {
        this.transform.position = Vector3.Slerp(previousPosition, this.transform.position, positionRatio);
        this.transform.rotation = Quaternion.Slerp(previousQuaternion, this.transform.rotation, rotationRatio);

        previousPosition = this.transform.position;
        previousQuaternion = this.transform.rotation;
    }
}
