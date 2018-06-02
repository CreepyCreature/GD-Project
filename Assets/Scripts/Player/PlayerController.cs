using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float speed = 250.0f;
    public Vector2 pointOfGravity = Vector2.zero;
    public float gravityConstant = 9.8f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _grounded;
    private Vector2 position2D
    {
        get { return transform.position; }
    }

	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ApplyGravity();

        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = transform.right * deltaX + transform.up * deltaY;
        _rigidbody.velocity = movement;

        ApplyGravity();
        CorrectOrientation();

        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1.0f, 1.0f);
        }

        CheckIfGrounded();  
        UpdateAnimator(deltaX, _grounded);
	}

    private void ApplyGravity()
    {
        Vector2 gravityDir = pointOfGravity - position2D;
        _rigidbody.AddForce(
            gravityDir.normalized * gravityConstant * 100.0f / gravityDir.sqrMagnitude);
    }

    private void CorrectOrientation()
    {
        transform.up = (position2D - pointOfGravity).normalized;
    }

    private void CheckIfGrounded()
    {
        float rayLength = 0.4f;
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
        int layerMask = 1 << 9; // 9 = Player layer
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, -transform.up, rayLength, ~layerMask);
        _grounded = hit.collider == null ? false : true;
    }

    private void UpdateAnimator(float deltaX, bool grounded)
    {
        _animator.SetFloat("speedX", _grounded ? Mathf.Abs(deltaX) : 0.0f);
        _animator.SetBool("grounded",_grounded);
    }
}
