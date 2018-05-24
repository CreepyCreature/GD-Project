using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public float radius = 2.0f;
	
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

        foreach (Collider2D collider in hit)
        {
            Debug.DrawRay(transform.position, 
                collider.transform.position - transform.position, 
                Color.red, 1.0f);

            Managers.PlayerResources.minerals += 0.1f * Time.deltaTime;
        }
    }
}
