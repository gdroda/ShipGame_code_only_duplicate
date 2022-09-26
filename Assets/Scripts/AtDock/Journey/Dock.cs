using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dock")]
public class Dock : ScriptableObject
{
    public string dockName;
    public string stringID;
    public CommodityPack[] commPackToSell;
    public Sailor[] sailorsToSell;
    public Ship[] shipsToSell;
    public Dock[] docksToSailTo;
    public float repairMod;
}

[System.Serializable]
public class CommodityPack
{
    public Commodity commodity;
    public float priceMod;

    //public CommodityPack(Commodity com, int mod)
    //{
    //    commodity = com;
    //    priceMod = mod;
    //}
}
