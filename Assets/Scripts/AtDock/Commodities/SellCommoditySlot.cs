using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellCommoditySlot : MonoBehaviour
{
    [SerializeField] private Commodity commodity;
    [SerializeField] private CommoditiesManager commoditiesManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText, valueText, earningText, amountText;
    private int amount;
    private float priceMod;
    private CommodityOnShip comOnShip;
    private const float sellbackCost = 0.06f;


    private void Start()
    {
        foreach (CommodityPack comm in playerRoster.currentDock.commPackToSell)
        {
            if (comm.commodity == commodity)
            {
                priceMod = comm.priceMod;
                UpdateUI();
                break;
            }
            else if (comm.commodity != commodity)
            {
                priceMod = 0.75f;
            }
        }
        UpdateUI();
    }

    public void SetCommodity(Commodity comm, int amt, CommodityOnShip _comOnShip)
    {
        commodity = comm;
        amount = amt;
        comOnShip = _comOnShip;
    }

    public void SellCommodity()
    {
        var price = commodity.cost * priceMod;
        
        playerRoster.GiveCoins(price * amount);
        playerRoster.ship.commoditiesOnShip.Remove(comOnShip);
        playerRoster.ship.CalculateStorageWeight();
        UpdateUI();
        commoditiesManager.CallUpdateUI();
        menuManager.OnCoinChange();
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        nameText.text = commodity.commodityName;
        var price = commodity.cost * (priceMod - sellbackCost);
        if (priceMod > 1) valueText.text = string.Format("<color=#3a8935>+{0:#.#}%</color>", ((priceMod - sellbackCost) - 1) * 100);
        else if (priceMod < 1) valueText.text = string.Format("<color=#da3e3e>-{0:#.#}%</color>", (1 - (priceMod - sellbackCost)) * 100);
        else valueText.text = string.Format("{0:#.#}%", (priceMod * 100));
        earningText.text = (price * amount).ToString("#,##0");
        amountText.text = $"x{amount}";
    }
}
