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

        float initialScale = Random.Range(2, 6);

        GetComponent<Rigidbody2D>().velocity = initialVelocity;
        transform.localScale = transform.localScale * initialScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Meteorite hit " + collision.gameObject);

        if (!collision.gameObject.CompareTag("Meteorite") &&
            !collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
