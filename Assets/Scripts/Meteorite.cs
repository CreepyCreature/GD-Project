using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Meteorite : MonoBehaviour
{
    private float _minerals = 0.1f;
    private float _scale = 1.0f;

    private void Start()
    {
        Vector3 initialVelocity = new Vector3(
            Random.Range(-2, 2), Random.Range(-2, 0), 0.0f);

        float initialScale = Random.Range(2, 4);
        _scale = initialScale;
        _minerals *= initialScale;

        GetComponent<Rigidbody2D>().velocity = initialVelocity;
        transform.localScale = transform.localScale * initialScale;

        Destroy(gameObject, 30.0f);
    }

    public float Mine(float speed)
    {
        float chips = speed * 0.1f * Time.deltaTime;
        if (_minerals - chips >= 0.0f)
        {
            _minerals -= chips;
            return chips;
        }
        Destroy(gameObject);
        return 0.0f;
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
