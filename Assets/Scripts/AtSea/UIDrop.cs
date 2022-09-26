using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrop : MonoBehaviour, IDropHandler
{
    private StationSlot stationSlot;

    private void Start()
    {
        stationSlot = GetComponent<StationSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            stationSlot.SetSailor(eventData.pointerDrag.GetComponent<SailorSlot>().GetSailor());
            //Debug.Log(name + " station got " + eventData.pointerDrag.GetComponent<SailorSlot>().GetSailor().sailorName);
        }
    }
}
