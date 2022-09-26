using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StationTools
{
    public static float GetStationLevel(Station.Stations station, Sailor sailor)
    {
        if (sailor != null)
        {
            switch (station)
            {
                case Station.Stations.Navigation:
                    return sailor.sailorAttributes.navigationLevel;
                case Station.Stations.Sailing:
                    return sailor.sailorAttributes.sailingLevel;
                case Station.Stations.Bilging:
                    return sailor.sailorAttributes.bilgingLevel;
                case Station.Stations.Carpenting:
                    return sailor.sailorAttributes.carpentingLevel;
                case Station.Stations.Gunning:
                    return sailor.sailorAttributes.gunningLevel;
                default:
                    return 0f; //temp?
            }
        }
        else return 0f; //temp?
    }
}
