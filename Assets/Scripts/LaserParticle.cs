using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticle : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 5.0f;

	void Start()
    {
        Destroy(gameObject, timeToLive);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meteorite"))
        {
            Destroy(collision.gameObject);
        }
        if (!collision.CompareTag("Player") &&
            !collision.CompareTag("NonPhysical"))
        {
            gameObject.SetActive(false);
        }
    }
}
