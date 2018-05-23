using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject meteoritePrefab;

    private GameObject _createdInstance;

	// Update is called once per frame
	void Update ()
    {
		if (_createdInstance == null)
        {
            _createdInstance = Instantiate(meteoritePrefab, transform) as GameObject;
        }
	}
}
