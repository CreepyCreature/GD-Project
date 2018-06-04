using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenLabel;
    [SerializeField] private TextMeshProUGUI mineralsLabel;
    [SerializeField] private TextMeshProUGUI shipLabel;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.AddListener(GameEvent.MINERALS_CHANGED, OnMineralsChanged);
        Messenger.AddListener(GameEvent.SHIP_REPAIR_PROGRESS_CHANGED, OnShipRepairProgressChanged);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.RemoveListener(GameEvent.MINERALS_CHANGED, OnMineralsChanged);
        Messenger.RemoveListener(GameEvent.SHIP_REPAIR_PROGRESS_CHANGED, OnShipRepairProgressChanged);
    }

    private void Start()
    {
        OnOxygenChanged();
        OnMineralsChanged();
        OnShipRepairProgressChanged();
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

    private void OnShipRepairProgressChanged()
    {
        shipLabel.text = "Ship Status: "
            + Mathf.RoundToInt(Managers.PlayerResources.shipRepairs * 100)
            + "%";
    }
}
