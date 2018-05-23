using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text oxygenLabel;
    [SerializeField] private Text mineralsLabel;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.AddListener(GameEvent.MINERALS_CHANGED, OnMineralsChanged);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.RemoveListener(GameEvent.MINERALS_CHANGED, OnMineralsChanged);
    }

    private void OnOxygenChanged()
    {
        oxygenLabel.text = "Oxygen Supply: " 
            + Mathf.RoundToInt(Managers.PlayerResources.oxygen * 100)
            + "%"; 
    }

    private void OnMineralsChanged()
    {
        mineralsLabel.text = "Mineral Supply: "
            + Mathf.RoundToInt(Managers.PlayerResources.minerals * 100)
            + "%";
    }
}
