using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField]
    private float oxygenCriticalTreshold = 0.25f;
    private bool _oxygenCritical = false;
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
            Messenger.Broadcast(GameEvent.OXYGEN_CHANGED, MessengerMode.DONT_REQUIRE_LISTENER);

            if (oxygen <= 0.0f)
            {
                Messenger.Broadcast(GameEvent.PLAYER_LOSE, MessengerMode.DONT_REQUIRE_LISTENER);
                Messenger.Broadcast(GameEvent.OXYGEN_DEPLETED, MessengerMode.DONT_REQUIRE_LISTENER);
            }
            else if (oxygen < oxygenCriticalTreshold && !_oxygenCritical)
            {
                Debug.Log("ResourceManager::O2Critical!");
                Messenger.Broadcast(GameEvent.OXYGEN_CRITICAL, MessengerMode.DONT_REQUIRE_LISTENER);
                _oxygenCritical = true;
            }
            else if (oxygen >= oxygenCriticalTreshold)
            {
                _oxygenCritical = false;
            }
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
            Messenger.Broadcast(GameEvent.MINERALS_CHANGED, MessengerMode.DONT_REQUIRE_LISTENER);
        }
    }

    private float _shipRepairs;
    public float shipRepairs
    {
        get { return _shipRepairs; }
        set
        {
            _shipRepairs = value;
            if (_shipRepairs > 1.0f)
                _shipRepairs = 1.0f;
            else if (_shipRepairs < 0.0f)
                _shipRepairs = 0.0f;

            Messenger.Broadcast(GameEvent.SHIP_REPAIR_PROGRESS_CHANGED, MessengerMode.DONT_REQUIRE_LISTENER);

            if (Mathf.Approximately(_shipRepairs, 1.0f))
            {
                Messenger.Broadcast(GameEvent.PLAYER_WIN, MessengerMode.DONT_REQUIRE_LISTENER);
            }
        }
    }

    public void Initialize()
    {
        status = ManagerStatus.Initializing;
        
        oxygen = 1.0f;
        minerals = 0.0f;
        shipRepairs = 0.1f;

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

    public bool MineralsMaxed()
    {
        return Mathf.Approximately(minerals, 1.0f);
    }

    public bool OxygenMaxed()
    {
        return Mathf.Approximately(oxygen, 1.0f);
    }

    public bool ShipRepairsMaxed()
    {
        return Mathf.Approximately(shipRepairs, 1.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            oxygen -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            oxygen += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            minerals -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            minerals += 0.1f;
        }
    }
}
