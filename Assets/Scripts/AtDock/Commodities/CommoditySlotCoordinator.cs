using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommoditySlotCoordinator : MonoBehaviour
{
    private CommoditySlot[] comSlotList;
    [SerializeField] private TextMeshProUGUI storageText, coinText, costSumText;
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private CommoditiesManager commoditiesManager;
    [SerializeField] private GameObject commodityLine;
    private int sumOfWeight = 0;
    private float sumOfCost = 0f;

    private void OnEnable()
    {
        commoditiesManager.onStorageChange.AddListener(UpdateStorageText);
    }

    private void OnDisable()
    {
        commoditiesManager.onStorageChange.RemoveListener(UpdateStorageText);
    }

    private void Start()
    {
        PopulatePanel();
        UpdateStorageText();
    }

    private void PopulatePanel()
    {
        var childList = new List<CommoditySlot>();
        foreach (CommodityPack comm in playerRoster.currentDock.commPackToSell)
        {
            var commPref = Instantiate(commodityLine, transform);
            var temp = commPref.GetComponent<CommoditySlot>();
            temp.SetCommodity(comm.commodity, comm.priceMod);
            childList.Add(temp);
        }
        comSlotList = childList.ToArray();
        commoditiesManager.comList = comSlotList;
    }

    private void UpdateStorageText()
    {
        UpdateCostText();
        sumOfWeight = playerRoster.ship.currentStorage;
        if (comSlotList != null)
        {
            foreach (CommoditySlot comSlot in comSlotList)
            {
                sumOfWeight += comSlot.GetAmountWeight();
            }
        }
        
        if (sumOfWeight == playerRoster.ship.currentStorage) storageText.text = $"Storage: <color=white>{playerRoster.ship.currentStorage}</color>/{playerRoster.ship.maxStorage}";
        else if (sumOfWeight > playerRoster.ship.maxStorage)
        {
            storageText.text = $"Storage: <color=#da3e3e>{sumOfWeight}</color>/{playerRoster.ship.maxStorage}";
        }
        else
            storageText.text = $"Storage: <color=yellow>{sumOfWeight}</color>/{playerRoster.ship.maxStorage}";
    }

    private void UpdateCostText()
    {
        sumOfCost = 0f;
        if (comSlotList != null)
        {
            foreach (CommoditySlot comSlot in comSlotList)
            {
                sumOfCost += comSlot.GetAmountPrice();
            }
        }

        costSumText.text = sumOfCost.ToString("#,##0");
    }
}
