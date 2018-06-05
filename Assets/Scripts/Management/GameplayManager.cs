using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public AudioClip danger1Clip;
    public AudioClip danger2Clip;

    private float _oxygenCriticalCooldown;

    public void Initialize()
    {
        status = ManagerStatus.Initializing;        
        status = ManagerStatus.Ready;
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OXYGEN_CRITICAL, OnOxygenCritical);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OXYGEN_CRITICAL, OnOxygenCritical);
    }

    private void OnOxygenCritical()
    {
        Managers.Audio.PlaySFX(danger2Clip);
    }
}
