using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceManager))]
[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour
{
    public static ResourceManager PlayerResources { get; private set; }
    public static AudioManager Audio { get; private set; }

    private List<IGameManager> _initSequence;

    private void Awake()
    {
        PlayerResources = GetComponent<ResourceManager>();
        Audio = GetComponent<AudioManager>();

        _initSequence = new List<IGameManager> { PlayerResources, Audio };

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _initSequence)
        {
            manager.Initialize();
        }

        yield return null;

        int numManagers = _initSequence.Count;
        int numReady = 0;

        while (numReady < numManagers)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _initSequence)
            {
                if (manager.status == ManagerStatus.Ready)
                {
                    ++numReady;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log("Managers: " + numReady + "/" + numManagers + " managers fired up.");
            }

            yield return null;
        }

        Debug.Log("All managers fired up!");
    }
}
