using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float radius = 2.0f;
    public float speed = 1.0f;

    public MineralParticle mineralParticlePrefab;
	
	void FixedUpdate ()
    {
		if (Input.GetButton("Mine Minerals") &&
            !Managers.PlayerResources.MineralsMaxed())
        {
            MineMinerals();
        }
	}

    private void MineMinerals()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius,
            LayerMask.GetMask("Mineable"));

        Collider2D meteoriteCollider = Array.Find(hit, p => p.CompareTag("Meteorite"));

        if (meteoriteCollider != null)
        {
            Debug.DrawRay(transform.position,
                meteoriteCollider.transform.position - transform.position,
                Color.red, 1.0f);

            var mineralParticle = Instantiate(mineralParticlePrefab, 
                meteoriteCollider.transform.position,
                Quaternion.identity);
            mineralParticle.MoveTowards(transform);
            
            Meteorite meteorite = meteoriteCollider.GetComponent<Meteorite>();
            Managers.PlayerResources.minerals += meteorite.Mine(speed);

            //Managers.PlayerResources.minerals += 0.1f * Time.deltaTime;
        }
    }
}
