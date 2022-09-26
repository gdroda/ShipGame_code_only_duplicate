using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship")]
public class Ship : ScriptableObject
{
    public string shipName;
    public string stringID;
    public Sprite icon;
    public Sprite shipModel;
    [Range(10f,30f)] public float speed; //NOT USED CURRENTLY
    public float maxDurability;
    public float maxDamagedDurability;
    public float currentDurability;
    public float maxBilge;
    public float currentBilge;
    public int maxStorage;
    public int currentStorage;
    public List<CommodityOnShip> commoditiesOnShip;
    public int sailorsMax;
    public ShipStations shipStations;
    [TextArea(3, 5)] public string flavorText;

    public Vector3 ShipPos { get; private set; }

    public void SetShipPos(Vector3 pos) => ShipPos = pos;

    private void OnEnable()
    {
        CalculateStorageWeight();
    }

    public void CalculateStorageWeight()
    {
        currentStorage = 0;
        foreach (CommodityOnShip com in commoditiesOnShip) //updating storage amount on its own on "enable", be wary of this
        {
            currentStorage += com.amount * com.commodity.weightPerUnit;
        }
    }
}

[System.Serializable]
public class CommodityOnShip
{
    public Commodity commodity;
    public int amount;

    public CommodityOnShip(Commodity com, int amt)
    {
        commodity = com;
        amount = amt;
    }
}

[System.Serializable]
public class ShipStations
{
    public int
    navigationStations,
    sailingStations,
    bilgingStations,
    carpentingStations,
    gunningStations,
    scoutStations,
    restStations;
}
