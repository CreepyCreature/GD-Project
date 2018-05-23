using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public float oxygen { get; private set; }
    public float minerals { get; private set; }

    public void Initialize()
    {
        status = ManagerStatus.Initializing;

        oxygen = 1.0f;
        minerals = 0.0f;

        status = ManagerStatus.Ready;
    }
}
