using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Commodity Library",menuName = "Libraries/Commodity Library")]
public class CommodityLibrary : ScriptableObject
{
    public Commodity[] commLibrary;

    public Commodity FindCommodity(string id)
    {
        foreach (Commodity comm in commLibrary)
        {
            if (comm.stringID == id)
                return comm;
        }
        Debug.Log($"Commodity {id} not found.");
        return null;
    }
}
