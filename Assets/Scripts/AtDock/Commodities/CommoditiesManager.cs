using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CommodityManager")]
public class CommoditiesManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent onStorageChange, onBuyUIUpdate;
    [System.NonSerialized] public UnityEvent<CommoditySlot[]> onGetCommodityList;
    public CommoditySlot[] comList;

    private void OnEnable()
    {
        if (onStorageChange == null) onStorageChange = new UnityEvent();
        if (onBuyUIUpdate == null) onBuyUIUpdate = new UnityEvent();
        if (onGetCommodityList == null) onGetCommodityList = new UnityEvent<CommoditySlot[]>();
    }

    public void OnAmountChange()
    { 
        onStorageChange.Invoke();
    }

    public void GetCommodityList()
    {
        onGetCommodityList.Invoke(comList);
    }

    public void CallUpdateUI()
    {
        onBuyUIUpdate.Invoke();
    }
    //Hold multiple commodity weight amount
    //Maybe rename to storageManager
    //Maybe hold cargo stuff
}
