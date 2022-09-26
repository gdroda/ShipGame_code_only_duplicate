using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalQuests : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private CommodityLibrary commLib;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText, commodityText, weightText, rewardText;
    [SerializeField] private Image commIcon;
    [SerializeField] private Button abandonButton;


    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        HideQuestUIElements();
        if (playerRoster.hasQuest)
        {
            titleText.text = playerRoster.currentQuest.title;
            if (commLib.FindCommodity(playerRoster.currentQuest.itemNeeded).icon != null) commIcon.sprite = commLib.FindCommodity(playerRoster.currentQuest.itemNeeded).icon;
            descriptionText.text = playerRoster.currentQuest.description;
            commodityText.text = $"x{playerRoster.currentQuest.itemQuantity} {commLib.FindCommodity(playerRoster.currentQuest.itemNeeded).commodityName}";
            weightText.text = $"Weight: {commLib.FindCommodity(playerRoster.currentQuest.itemNeeded).weightPerUnit * playerRoster.currentQuest.itemQuantity} kg";
            rewardText.text = "Reward: " + playerRoster.currentQuest.reward.ToString("#,##0");
            commIcon.sprite = commLib.FindCommodity(playerRoster.currentQuest.itemNeeded).icon;
        }
        else
        {
            titleText.text = "No active quests";
            descriptionText.text = "Go get a new one!";
        }
    }

    private void HideQuestUIElements()
    {
        if (playerRoster.hasQuest)
        {
            commodityText.gameObject.SetActive(true);
            weightText.gameObject.SetActive(true);
            rewardText.gameObject.SetActive(true);
            commIcon.gameObject.SetActive(true);
            abandonButton.gameObject.SetActive(true);
        }
        else
        {
            commodityText.gameObject.SetActive(false);
            weightText.gameObject.SetActive(false);
            rewardText.gameObject.SetActive(false);
            commIcon.gameObject.SetActive(false);
            abandonButton.gameObject.SetActive(false);
        }
    }

    public void AbandonQuest()
    {
        if (playerRoster.hasQuest)
        {
            playerRoster.currentQuest = null;
            playerRoster.hasQuest = false;
            UpdateText();
            Debug.Log("Quest Abandoned");
            DataPersistenceManager.instance.SaveGame();
        }
    }
}
