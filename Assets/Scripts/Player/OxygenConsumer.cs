using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenConsumer : MonoBehaviour
{
    // 1/consumption = time to last
    public float consumption = 0.016f;
    

	// Update is called once per frame
	void Update ()
    {
        Managers.PlayerResources.ConsumeOxygen(consumption * Time.deltaTime);
	}
}
