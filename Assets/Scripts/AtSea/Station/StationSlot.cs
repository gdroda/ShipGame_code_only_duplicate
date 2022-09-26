using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //remove when not using text

public class StationSlot : MonoBehaviour
{
    public Station.Stations station;
    [SerializeField] private int slotAmount;
    [SerializeField] StationManager stationManager;
    [SerializeField] SailingManager sailingManager;

    private List<Sailor> sailorsOnStation = new List<Sailor>();

    private void OnEnable()
    {
        stationManager.onStationChange.AddListener(RemoveSailor);
    }
    
    private void OnDisable()
    {
        stationManager.onStationChange.RemoveListener(RemoveSailor);
    }

    private void Start()
    {
        sailorsOnStation.Clear();
    }

    private void RemoveSailor(Sailor stationChanger)
    {
        if (sailorsOnStation.Contains(stationChanger))
        {
            sailorsOnStation.Remove(stationChanger);
            stationChanger.currentStation = Station.Stations.None;
            stationChanger.stationIcon = null;
            StationAction(stationChanger);
        }
        //else Debug.Log("doesnt contain?");
    }

    public void SetSailor(Sailor sailGuy)
    {
        stationManager.StationChanged(sailGuy);
        if (sailorsOnStation.Count <= slotAmount - 1)
        {
            sailorsOnStation.Add(sailGuy); //add sailor if there is a spot and he's not already there
            StationSet(sailGuy); //need to remove him from previous position if exists
            StationAction(sailGuy);
        }
        else
        {
            Debug.Log("Station has no available spots.");
            return;
        }
    }

    private void StationSet(Sailor sailor)
    {
        sailor.currentStation = station;
        sailor.IconChange(stationManager.GetStationIcon(station));
        sailor.UIChange(TempStationIcon());
        if (station == Station.Stations.None) sailor.onStation = false;
        else sailor.onStation = true;
        Debug.Log(sailor.sailorName + " placed on a " + station + " station.");
    }

    private string TempStationIcon()  //REMOVE THIS AND SAILOR UICHANGE WHEN ALL ICONS ARE PRESENT!
    {
        string stationID = station switch
        {
            Station.Stations.Navigation => "",
            Station.Stations.Sailing => "",
            Station.Stations.Bilging => "Bi",
            Station.Stations.Carpenting => "",
            Station.Stations.Resting => "Re",
            Station.Stations.None => throw new System.NotImplementedException(),
            Station.Stations.Gunning => "",
            Station.Stations.Scouting => throw new System.NotImplementedException(),
            _ => "-",
        };
        return stationID;
    }

    //public Sailor GetSailor() {return sailor;} SO IS THIS NEEDED?

    private void StationAction(Sailor sailorer)
    {
        switch (station)
        {
            case Station.Stations.Navigation:
                sailingManager.onNav = sailorsOnStation.Contains(sailorer);
                break;
            case Station.Stations.Sailing: //change bilingMod between value and 0

                if (sailorsOnStation.Contains(sailorer))
                {
                    sailingManager.sailingMod += StationTools.GetStationLevel(station, sailorer);
                }
                else sailingManager.sailingMod -= StationTools.GetStationLevel(station, sailorer);
                break;
            case Station.Stations.Bilging: //change bilingMod between value and 0
                if (sailorsOnStation.Contains(sailorer))
                {
                    sailingManager.bilgingMod += StationTools.GetStationLevel(station, sailorer);
                }
                else sailingManager.bilgingMod -= StationTools.GetStationLevel(station, sailorer);
                break;
            case Station.Stations.Carpenting: //change carpentingMod between value and 0
                if (sailorsOnStation.Contains(sailorer))
                {
                    sailingManager.carpentingMod += StationTools.GetStationLevel(station, sailorer);
                }
                else sailingManager.carpentingMod -= StationTools.GetStationLevel(station, sailorer);
                break;
            case Station.Stations.Gunning:
                if (sailorsOnStation.Contains(sailorer)) //change gunningMod between value and 0
                {
                    sailingManager.gunningMod += StationTools.GetStationLevel(station, sailorer);
                }
                else sailingManager.gunningMod -= StationTools.GetStationLevel(station, sailorer);
                break;
        }
    }
}