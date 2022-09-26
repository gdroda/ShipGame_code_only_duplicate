using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestGenerator : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private CommodityLibrary commLib;
    [SerializeField] private DockLibrary dockLib;
    [SerializeField] private GameObject receivePanel, turnInPanel, abandonPanel;

    private void Start()
    {
        PanelSwitching();
    }

    public void PanelSwitching()
    {
        if (!playerRoster.hasQuest)
        {
            PanelsOff();
            receivePanel.SetActive(true);
            //questManager.TextUpdate();
            CreateQuest();
        }
        else if (playerRoster.currentDock == dockLib.FindDock(playerRoster.currentQuest.destination) && playerRoster.hasQuest)
        {
            PanelsOff();
            turnInPanel.SetActive(true);
            questManager.TextUpdate();
        }
        else
        {
            PanelsOff();
            abandonPanel.SetActive(true);
            questManager.TextUpdate();
        }
    }

    private void PanelsOff()
    {
        receivePanel.SetActive(false);
        turnInPanel.SetActive(false);
        abandonPanel.SetActive(false);
    }

    public void NewQuest()
    {
        CreateQuest();
    }

    private void CreateQuest()
    {
        Quest qq = new Quest();
        qq.title = "Commodity Transport";
        qq.itemNeeded = PickCommInDock().stringID; //commLib.commLibrary[Random.Range(0, commLib.commLibrary.Length)];
        qq.itemQuantity = Random.Range(1, 51);
        qq.destination = PickOtherDock().stringID;
        qq.reward = commLib.FindCommodity(qq.itemNeeded).cost /*qq.itemNeeded.cost*/ * Random.Range(1.2f,1.6f) * qq.itemQuantity; //comm cost * amount * random multiplier between 1.2 and 1.6
        qq.description = $"We'd like you to transport some <color=blue>{commLib.FindCommodity(qq.itemNeeded).commodityName/*qq.itemNeeded.commodityName*/}</color> to <color=blue>{dockLib.FindDock(qq.destination).dockName}</color>. You'll have to come up with the quantity yourself though.";
        questManager.QuestCreated(qq);
    }

    private Commodity PickCommInDock()
    {
        List<Commodity> commsInDock = new List<Commodity>();
        foreach (CommodityPack comm in playerRoster.currentDock.commPackToSell)
        {
            commsInDock.Add(comm.commodity);
        }

        List<Commodity> commsNotInDock = commLib.commLibrary.ToList<Commodity>();
        foreach (Commodity comm in commLib.commLibrary)
        {
            if (commsInDock.Contains(comm))
            {
                commsNotInDock.Remove(comm);
            }    
        }
        Commodity temp;
        do
        {
            temp = commLib.commLibrary[Random.Range(0, commLib.commLibrary.Length)];
        } while (commsNotInDock.Contains(temp));  // add ! if commodity not in current dock, remove ! if need commodity in current dock
        return temp;
    }

    private Dock PickOtherDock()
    {
        Dock temp;
        do
        {
            temp = dockLib.dockLib[Random.Range(0, dockLib.dockLib.Length)];
        } while (temp == playerRoster.currentDock);
        return temp;
    }

    private void Update() //TEMP DELETE THIS
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerRoster.currentQuest != null)
            {
                Debug.Log(playerRoster.currentQuest.itemNeeded + " " + playerRoster.currentQuest.itemQuantity);
            }
            else Debug.Log("no quests.");
        }
    }
}

[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    /**/
    public string itemNeeded;
    public int itemQuantity;
    public string destination;
    public float reward;

    public Quest() { }

    public Quest(string _title, string _description, /**/string _itemNeeded, int _itemQuantity, string _destination)
    {
        title = _title;
        description = _description;
        itemNeeded = _itemNeeded;
        itemQuantity = _itemQuantity;
        destination = _destination;

        
    }
    
}
