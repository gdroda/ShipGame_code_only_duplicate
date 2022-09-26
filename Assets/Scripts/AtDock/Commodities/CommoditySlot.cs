using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommoditySlot : MonoBehaviour
{
    [SerializeField] private Commodity commodity;
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] TextMeshProUGUI commodityName, commodityCost, commodityWeightInfo, commodityPriceMod, commodityUnitPrice;
    [SerializeField] Image commodityIcon; 
    [SerializeField] TMP_InputField inputField;
    [SerializeField] CommoditiesManager commoditiesManager;
    private int amount;
    private float priceMod;

    private void OnEnable()
    {
        commoditiesManager.onBuyUIUpdate.AddListener(OnBuyUpdate);
    }

    private void OnDisable()
    {
        commoditiesManager.onBuyUIUpdate.RemoveListener(OnBuyUpdate);
    }

    private void Start()
    {
        UpdateUI();
        inputField.text = "";
    }

    public void UpdateUI()
    {
        commodityName.text = commodity.commodityName;
        commodityCost.text = (commodity.cost * amount * priceMod).ToString("#,##0");
        //if (priceMod > 1) commodityPriceMod.text = $"<color=#3a8935>+{(priceMod - 1) * 100}%</color>";
        if (priceMod > 1) commodityPriceMod.text = string.Format("<color=#da3e3e>+{0:#.#}%</color>", (priceMod - 1) * 100);
        else if (priceMod < 1) commodityPriceMod.text = string.Format("<color=#3a8935>-{0:#.#}%</color>", (1 - priceMod) * 100);
        else commodityPriceMod.text = string.Format("{0:#.#}%", (priceMod * 100));
        commodityUnitPrice.text = (commodity.cost * priceMod).ToString("#,##0");
        commodityWeightInfo.text = $"{commodity.weightPerUnit} kg / Unit";
        commodityIcon.sprite = commodity.icon;
        commoditiesManager.OnAmountChange();
    }

    public void SetAmount()
    {
        if (inputField.text == "0") inputField.text = "";

        int.TryParse(inputField.text, out amount);
        UpdateUI();
    }

    private void OnBuyUpdate()
    {
        UpdateUI();
        inputField.text = "";
    }

    public int GetAmount() => amount;

    public int GetAmountWeight() => amount * commodity.weightPerUnit;

    public float GetAmountPrice() => amount * commodity.cost * priceMod;

    public Commodity GetCommodity() => commodity;

    public void SetCommodity(Commodity _commodity, float _priceMod)
    {
        commodity = _commodity;
        priceMod = _priceMod;
    }

    public void AddUnit()
    {
        if (inputField.text == "")
        {
            amount++;
            inputField.text = amount.ToString();
        }
        else if (inputField.text != "")
        {
            int.TryParse(inputField.text, out amount);
            amount++;
            inputField.text = amount.ToString();
        }
    }

    public void RemoveUnit()
    {
        if (inputField.text != "")
        {
            int.TryParse(inputField.text, out amount);
            amount--;
            inputField.text = amount.ToString();
        }
    }
}
