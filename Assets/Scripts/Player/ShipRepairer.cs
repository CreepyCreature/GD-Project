using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairer : MonoBehaviour
{
    public float radius = 1.0f;

    [Range(0, 1)]
    public float repairSpeed = 0.1f;

	void Update ()
    {
		if (Input.GetButton("Repair Ship") &&
            !Managers.PlayerResources.ShipRepairsMaxed())
        {
            RepairShip();
        }
	}

    private void RepairShip()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius,
            LayerMask.GetMask("Ship"));
        if (hit == null)
        {
            return;
        }

        Debug.DrawRay(transform.position, hit.transform.position - transform.position,
            Color.green, 1.0f);

        float amount = repairSpeed * Time.deltaTime;
        if (Managers.PlayerResources.minerals >= amount)
        {
            Managers.PlayerResources.minerals -= amount;
            Managers.PlayerResources.shipRepairs += amount / 2.0f;
        }
        else
        {
            Debug.Log("NOT ENOUGH MINERALS!");
        }
    }
}
