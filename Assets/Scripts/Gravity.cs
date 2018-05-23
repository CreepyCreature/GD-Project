using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Gravity : MonoBehaviour
{
    public Vector2 pointOfGravity = Vector2.zero;
    public float gravityConstant = 9.8f;

    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
        Vector2 gravityDir = pointOfGravity - (Vector2)transform.position;
        _rigidbody.AddForce(
            gravityDir.normalized * gravityConstant * 100.0f / gravityDir.sqrMagnitude);
    }
}
