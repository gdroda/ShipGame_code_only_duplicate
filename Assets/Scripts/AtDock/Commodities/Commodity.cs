using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Commodity")]
public class Commodity : ScriptableObject
{
    public string commodityName;
    public string stringID;
    public float cost;
    public int weightPerUnit;
    public Sprite icon;
}
