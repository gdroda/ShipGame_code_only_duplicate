using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRoster")]
public class PlayerRoster : ScriptableObject, IDataPersistence
{
    public Ship ship;
    public Sailor[] sailors;
    public Dock currentDock;
    public Dock destinationDock;
    public Quest currentQuest; //change this?
    public bool hasQuest = false;
    public float currentDistanceToTravel;
    public float distanceTraveled;

    [SerializeField] private float coins; //serialize field used to check on coins for debugging mainly.
    [SerializeField] private ShipLibrary shipLibrary;
    [SerializeField] private DockLibrary dockLibrary;
    [SerializeField] private SailorLibrary sailorLibrary;
    public void GiveCoins(float _coins)
    {
        coins += _coins;
    }

    public float GetCoins()
    {
        return coins;
    }

    public void LoadData(GameData data)
    {
        this.coins = data.coins;
        this.ship = shipLibrary.FindShip(data.shipID);
        this.sailors = IDToSailor(data.sailorsID);
        this.currentDock = dockLibrary.FindDock(data.currentDockID);
        this.hasQuest = data.hasQuest;
        if (hasQuest) this.currentQuest = data.quest;
    }

    public void SaveData(ref GameData data)
    {
        data.coins = this.coins;
        data.shipID = this.ship.stringID;
        data.sailorsID = SailorToID();
        data.currentDockID = this.currentDock.stringID;
        data.hasQuest = this.hasQuest;
        if (hasQuest) data.quest = this.currentQuest;
    }

    private Sailor[] IDToSailor(string[] sailors)
    {
        List<Sailor> sailorList = new List<Sailor>();
        foreach (string sailor in sailors)
        {
            sailorList.Add(sailorLibrary.FindSailor(sailor));
        }
        return sailorList.ToArray();
    }

    private string[] SailorToID()
    {
        List<string> sailorList = new List<string>();
        foreach (Sailor sailor in sailors)
        {
            sailorList.Add(sailor.stringID);
        }
        return sailorList.ToArray();
    }
}
