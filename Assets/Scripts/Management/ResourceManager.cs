using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private float _oxygen;
    public float oxygen
    {
        get { return _oxygen; }
        set
        {
            _oxygen = value;
            if (_oxygen > 1.0f)
                _oxygen = 1.0f;
            else if (_oxygen < 0.0f)
                _oxygen = 0.0f;

            // (see http://wiki.unity3d.com/index.php?title=CSharpMessenger_Extended)
            Messenger.Broadcast(GameEvent.OXYGEN_CHANGED);
        }
    }

    private float _minerals;
    public float minerals
    {
        get { return _minerals; }
        set
        {
            _minerals = value;
            if (_minerals > 1.0f)
                _minerals = 1.0f;
            else if (_minerals < 0.0f)
                _minerals = 0.0f;

            // (see http://wiki.unity3d.com/index.php?title=CSharpMessenger_Extended)
            Messenger.Broadcast(GameEvent.MINERALS_CHANGED);
        }
    }

    public void Initialize()
    {
        status = ManagerStatus.Initializing;

        oxygen = 1.0f;
        minerals = 0.0f;

        status = ManagerStatus.Ready;
    }

    public void ConsumeOxygen(float amount)
    {
        oxygen -= amount;
    }

    public void AdjustMinerals(float amount)
    {
        minerals += amount;
    }
}
