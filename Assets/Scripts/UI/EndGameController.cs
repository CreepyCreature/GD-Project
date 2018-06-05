using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour {
    [SerializeField]
    private int oxygenValue;
    [SerializeField]
    private int shipHP;
    [SerializeField]
    public Animator gameOverAnimator;
    [SerializeField]
    private TextMeshProUGUI backgroundText;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI gameStatus;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private SpriteRenderer shipRenderer;
    [SerializeField]
    private List<Sprite> shipStatus;


    private void Awake()
    {
        Messenger.AddListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.AddListener(GameEvent.SHIP_REPAIR_PROGRESS_CHANGED, OnShipRepairProgressChanged);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OXYGEN_CHANGED, OnOxygenChanged);
        Messenger.RemoveListener(GameEvent.SHIP_REPAIR_PROGRESS_CHANGED, OnShipRepairProgressChanged);
    }

    private void OnOxygenChanged()
    {
        oxygenValue = Mathf.RoundToInt(Managers.PlayerResources.oxygen * 100);
        if(oxygenValue == 0)
        {
            GameOver();
        }
    }

    private void OnShipRepairProgressChanged()
    {
        shipHP = Mathf.RoundToInt(Managers.PlayerResources.shipRepairs * 100);

        if(shipHP == 0)
        {
            //GameOver();
        }
        if(shipHP == 100)
        {
            Victory();
        }

        if(shipHP < 30)
        {
            shipRenderer.sprite = shipStatus[2];
        }
        else if(shipHP < 85)
        {
            shipRenderer.sprite = shipStatus[1];
        }
        else
        {
            shipRenderer.sprite = shipStatus[0];
        }
    }

    private void GameOver()
    {
        gameStatus.text = "GAME OVER";
        buttonText.text = "Retry";
        backgroundText.text = "You failed to repair the ship in time to save yourself";
        gameOverAnimator.SetBool("GameOver", true);
        player.GetComponent<PlayerController>().speed = 0;
        player.GetComponent<Miner>().radius = 0;
        player.GetComponent<OxygenConsumer>().consumption = 0;
    }

    private void Victory()
    {
        gameStatus.text = "Congratulations";
        buttonText.text = "Play Again";
        backgroundText.text = "You managed to repair the ship in time to save yourself";
        gameOverAnimator.SetBool("GameOver", true);
        player.GetComponent<PlayerController>().speed = 0;
        player.GetComponent<Miner>().radius = 0;
        player.GetComponent<OxygenConsumer>().consumption = 0;
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
