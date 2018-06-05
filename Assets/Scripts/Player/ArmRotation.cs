using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public float rotationOffset = 0;
    public PlayerController Player;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        //Debug.Log(Player.deltaX);
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if(Player.goingRight)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset + 180);
        }
        
    }
}
