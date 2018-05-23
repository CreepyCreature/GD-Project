using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Meteorite : MonoBehaviour
{
    private void Start()
    {
        Vector3 initialVelocity = new Vector3(
            Random.Range(-2, 2), Random.Range(-2, 0), 0.0f);     

        GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Meteorite hit " + collision.gameObject);

        Destroy(gameObject);
    }
}
