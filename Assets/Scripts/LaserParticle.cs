using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticle : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 5.0f);
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Meteorite"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
}
