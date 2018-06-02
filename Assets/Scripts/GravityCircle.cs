using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCircle : MonoBehaviour
{
    private float gravityPower = 1000.00f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.IsChildOf(collision.transform) || collision.attachedRigidbody == null)
        {
            return;
        }

        Vector2 attractionForce = transform.position - collision.transform.position;
        attractionForce = attractionForce.normalized * gravityPower * Time.deltaTime;

        collision.attachedRigidbody.AddForce(attractionForce);
    }
}
