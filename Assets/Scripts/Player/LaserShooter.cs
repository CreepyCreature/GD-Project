using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObject particlePrefab;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(transform.position, mouseWorldPos, Color.cyan);

            Vector3 spawnDir = mouseWorldPos - transform.position;
            Vector3 spawnPos = transform.position + spawnDir.normalized;

            GameObject particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
            particle.GetComponent<Rigidbody2D>().velocity = (Vector2)spawnDir + _rigidbody.velocity;
        }
	}
}
