using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float speed = 250.0f;
    public Vector2 pointOfGravity = Vector2.zero;
    public float gravityConstant = 9.8f;
    public bool goingRight = true;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _grounded;
    private Vector2 position2D
    {
        get { return transform.position; }
    }

    protected float _deltaX;
    public float deltaX
    {
        get { return _deltaX; }
        set { _deltaX = value; }
    }

	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        goingRight = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ApplyGravity();

        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = transform.right * deltaX + transform.up * deltaY;
        _rigidbody.velocity = movement;

        //MouseMovement();

        ApplyGravity();
        CorrectOrientation();

        if (!Mathf.Approximately(deltaX, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1.0f, 1.0f);
            if(transform.localScale.x == 1)
            {
                goingRight = true;
            }
            else
            {
                goingRight = false;
            }
        }

        CheckIfGrounded();
        UpdateAnimator(_rigidbody.velocity.magnitude, _grounded);
        //UpdateAnimator(deltaX, _grounded);
    }

    private void MouseMovement()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mouseWorldPos, Color.green);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 moveDirection = Vector3.Normalize(mouseWorldPos - transform.position);
            _rigidbody.velocity = _rigidbody.velocity + 
                (Vector2)(moveDirection * speed * 2 * Time.deltaTime);

            Debug.Log(Vector3.Dot(transform.right, moveDirection));
            float scaleX = Vector3.Dot(transform.right, moveDirection) > 0.0f ? 1.0f : -1.0f;
            Debug.Log(scaleX);
            transform.localEulerAngles = new Vector3(scaleX, 1.0f, 1.0f);
        }
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
