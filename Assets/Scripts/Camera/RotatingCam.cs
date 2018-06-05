using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCam : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private void LateUpdate()
    {
        transform.rotation = target.rotation;
    }
}
