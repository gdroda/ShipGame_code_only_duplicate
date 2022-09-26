using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StationManager")]
public class StationManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<Sailor> onStationChange;

    private void OnEnable()
    {
        if (onStationChange == null)
            onStationChange = new UnityEvent<Sailor>();
    }

    public void StationChanged(Sailor sailor)
    {
        onStationChange.Invoke(sailor);
    }

    public Sprite[] stationIcons;

    public Sprite GetStationIcon(Station.Stations station)
    {
        switch (station)
        {
            case Station.Stations.Navigation:
                return stationIcons[0];
            case Station.Stations.Sailing:
                return stationIcons[1];
            case Station.Stations.Carpenting:
                return stationIcons[2];
            case Station.Stations.Gunning:
                return stationIcons[3];
            default:
                return null;
        }
    }
}
