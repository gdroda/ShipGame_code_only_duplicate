using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SailorUIUpdate();
[CreateAssetMenu(fileName = "Sailor")]
public class Sailor : ScriptableObject
{
    public SailorUIUpdate onSailorUIUpdate;
    public string sailorName;
    public string stringID;
    public Sprite icon;
    /*[System.NonSerialized]*/ public Station.Stations currentStation = Station.Stations.None;
    public Sprite stationIcon; //maybe like this?
    [System.NonSerialized] public string stationText;
    [Range(0f, 100f)] public float maxFatigue = 100f;
    [Range(0f, 100f)] public float fatigue = 100f;
    public bool onStation = false;
    public enum Diseases
    {
        none,
        smallPox,
        yellowFever,
        typhus,
        malaria,
        pneumonia,
        typhoid
    }
    public Diseases disease;
    public SailorAttributes sailorAttributes;
    [TextArea(3, 5)] public string flavorText;

    public void UIChange(string textu) //temporary way to change stationText with event
    {
        if (textu != null)
        {
            stationText = textu;
            onSailorUIUpdate?.Invoke();
        }
    }

    public void IconChange(Sprite icon) //temporary way to change stationText with event
    {
        if (icon != null)
        {
            stationIcon = icon;
            onSailorUIUpdate?.Invoke();
        }
    }
}

[System.Serializable]
public class SailorAttributes
{
    [Range(0, 10)]
    public float
    navigationLevel,
    sailingLevel,
    bilgingLevel,
    carpentingLevel,
    gunningLevel,
    treasureHaulingLevel,
    brawlingLevel,
    swordmanshipLevel,
    medicineLevel,
    scoutLevel;
}