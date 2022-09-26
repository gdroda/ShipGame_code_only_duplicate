using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float coins;
    public string shipID;
    public string[] sailorsID;
    public string currentDockID;
    public Quest quest;
    public bool hasQuest;
    public string[] chartedNodes;

    public GameData(string shipID, string currentDockID, string[] sailors, bool hasQuest)
    {
        this.coins = 10000f; //default on new game
        this.shipID = shipID;
        this.currentDockID = currentDockID;
        this.sailorsID = sailors;
        this.hasQuest = hasQuest;
    }
}
