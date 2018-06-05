using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCam : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public bool smoothOut = true;

    public float smoothTime = 0.4f;
    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (smoothOut)
        {
            NonVomitingRotation();
        }
        else
        {
            VomitingRotation();
        }
    }

    private void VomitingRotation()
    {
        transform.rotation = target.rotation;
    }

    private void NonVomitingRotation()
    {
        Vector3 targetRotation = new Vector3(
            transform.localEulerAngles.x, transform.localEulerAngles.y, target.localEulerAngles.z);

        transform.localEulerAngles = Vector3.SmoothDamp(transform.localEulerAngles,
            targetRotation, ref _velocity, smoothTime);
    }
}
