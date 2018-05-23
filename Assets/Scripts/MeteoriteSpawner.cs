using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    public GameObject meteoritePrefab;

    public float spawnInterval = 3.0f;
    public bool spawnSingle = false;    

    private GameObject _createdInstance;

    private void Start()
    {
        if (!spawnSingle)
        {
            StartCoroutine(SpawnInteval(spawnInterval));
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (spawnSingle)
        {
            SpawnSingle();
        }
	}

    private IEnumerator SpawnInteval(float interval)
    {
        while (true)
        {
            Instantiate(meteoritePrefab, transform);

            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnSingle()
    {
        if (_createdInstance == null)
        {
            _createdInstance = Instantiate(meteoritePrefab, parent: transform) as GameObject;
        }
    }
}
