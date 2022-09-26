using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Station
{
    public enum Stations
    {
        None,
        Navigation,
        Sailing,
        Bilging,
        Carpenting,
        Gunning,
        Scouting,
        Resting
    }
}
