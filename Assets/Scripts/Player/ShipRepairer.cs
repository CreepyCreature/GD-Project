using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairer : MonoBehaviour
{
    public MineralParticle repairParticlePrefab;

    public float radius = 1.0f;

    [Range(0, 1)]
    public float repairSpeed = 0.1f;

	void Update ()
    {
		if (Input.GetButton("Repair Ship") &&
            !Managers.PlayerResources.ShipRepairsMaxed())
        {
            //RepairShip();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            MouseRepairShip();
        }
	}

    private void MouseRepairShip()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(transform.position, mouseWorldPos, Color.yellow);

        if (Vector2.Distance(mouseWorldPos, transform.position) < radius)
        {
            Collider2D shipCollider = Physics2D.OverlapPoint(mouseWorldPos,
                LayerMask.GetMask("Ship"));
            if (shipCollider == null)
            {
                return;
            }

            float amount = repairSpeed * Time.deltaTime;
            if (Managers.PlayerResources.minerals >= amount)
            {
                Debug.Log("Repairing ship..");

                Managers.PlayerResources.minerals -= amount;
                Managers.PlayerResources.shipRepairs += amount / 2.0f;

                MineralParticle repairParticle = Instantiate(repairParticlePrefab,
                    transform.position, Quaternion.identity);
                repairParticle.MoveTowards(shipCollider.transform);
            }
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

            MineralParticle repairParticle = Instantiate(repairParticlePrefab,
                                                transform.position, Quaternion.identity);
            repairParticle.MoveTowards(hit.transform);
        }
        else
        {
            Debug.Log("NOT ENOUGH MINERALS!");
        }
    }
}
