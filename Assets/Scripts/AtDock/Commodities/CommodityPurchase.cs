using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommodityPurchase : MonoBehaviour
{
    [SerializeField] private CommoditiesManager commoditiesManager;
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private MenuManager menuManager;
    private CommoditySlot[] comList;

    private void OnEnable()
    {
        commoditiesManager.onGetCommodityList.AddListener(GetComList);
    }

    private void OnDisable()
    {
        commoditiesManager.onGetCommodityList.RemoveListener(GetComList);
    }

    private void GetComList(CommoditySlot[] list)
    {
        comList = list;
    }

    public void CommodityBuy()
    {
        commoditiesManager.GetCommodityList();
        float totalCost = 0;
        int totalWeight = 0;
        foreach (CommoditySlot comSlot in comList)
        {
            totalCost += comSlot.GetAmountPrice();
            totalWeight += comSlot.GetAmountWeight();
        }


        if (totalCost < playerRoster.GetCoins())
        {
            if (totalWeight + playerRoster.ship.currentStorage <= playerRoster.ship.maxStorage)
            {
                foreach (CommoditySlot comSlot in comList)
                {
                    if (comSlot.GetAmount() > 0)
                    {
                        foreach (CommodityOnShip comOn in playerRoster.ship.commoditiesOnShip) //checking if commodity already exists
                        {
                            if (comOn.commodity == comSlot.GetCommodity())
                            {
                                comOn.amount += comSlot.GetAmount();
                                playerRoster.ship.CalculateStorageWeight();
                                playerRoster.GiveCoins(-comSlot.GetAmountPrice());
                                commoditiesManager.CallUpdateUI();
                                menuManager.OnCoinChange();
                                return;
                            }
                        }
                        playerRoster.ship.commoditiesOnShip.Add(new CommodityOnShip(comSlot.GetCommodity(), comSlot.GetAmount())); //if not add new
                        playerRoster.ship.CalculateStorageWeight();
                        playerRoster.GiveCoins(-comSlot.GetAmountPrice());
                    }
                }
                commoditiesManager.CallUpdateUI();
                menuManager.OnCoinChange();
            }
            else Debug.Log("Not enough space!"); //else message about not enough storage
        }
        else Debug.Log("Not enough coins"); //else message about not enough coins
    }
}
