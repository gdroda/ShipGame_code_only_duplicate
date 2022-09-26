using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private CommodityLibrary commLib;
    [SerializeField] private TextMeshProUGUI titleText, descriptionText, commText, rewardText, weightText;
    [SerializeField] private Image icon;
    [SerializeField] private UnityEvent changePanel;
    private Quest quest;

    private void OnEnable()
    {
        questManager.onQuestCreation.AddListener(UpdateQuestText);
        questManager.onPanelTextUpdate.AddListener(UpdateQuestText);
        if (changePanel == null) changePanel = new UnityEvent();
    }

    private void OnDisable()
    {
        questManager.onQuestCreation.RemoveListener(UpdateQuestText);
        questManager.onPanelTextUpdate.RemoveListener(UpdateQuestText);
    }

    private void UpdateQuestText()
    {
        if (playerRoster.hasQuest)
        {
            quest = playerRoster.currentQuest;
            titleText.text = quest.title;
            if (commLib.FindCommodity(quest.itemNeeded).icon != null) icon.sprite = commLib.FindCommodity(quest.itemNeeded).icon;
            descriptionText.text = quest.description;
            commText.text = $"x{quest.itemQuantity} {commLib.FindCommodity(quest.itemNeeded).commodityName}";
            rewardText.text = quest.reward.ToString("#,##0");
            weightText.text = $"{commLib.FindCommodity(quest.itemNeeded).weightPerUnit * quest.itemQuantity} kg needed";
        }
    }

    private void UpdateQuestText(Quest _quest)
    {
        quest = _quest;
        titleText.text = quest.title;
        if (commLib.FindCommodity(quest.itemNeeded).icon != null) icon.sprite = commLib.FindCommodity(quest.itemNeeded).icon;
        descriptionText.text = quest.description;
        commText.text = $"x{quest.itemQuantity} {commLib.FindCommodity(quest.itemNeeded).commodityName}";
        rewardText.text = quest.reward.ToString("#,##0");
        weightText.text = $"{commLib.FindCommodity(quest.itemNeeded).weightPerUnit * quest.itemQuantity} kg needed";
    }

    public void AcceptQuest()
    {
        if (!playerRoster.hasQuest)
        {
            playerRoster.currentQuest = quest;
            playerRoster.hasQuest = true;
            changePanel.Invoke();
            Debug.Log("Quest Received!");
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void TurnInQuest()
    {
        foreach (CommodityOnShip comm in playerRoster.ship.commoditiesOnShip)
        {
            if (comm.commodity == commLib.FindCommodity(playerRoster.currentQuest.itemNeeded))
            {
                if (comm.amount >= playerRoster.currentQuest.itemQuantity)
                {
                    playerRoster.GiveCoins(playerRoster.currentQuest.reward);
                    if (comm.amount > playerRoster.currentQuest.itemQuantity)
                    {
                        comm.amount -= playerRoster.currentQuest.itemQuantity;
                    }
                    else
                    {
                        playerRoster.ship.commoditiesOnShip.Remove(comm);
                    }
                    playerRoster.ship.CalculateStorageWeight();
                    playerRoster.currentQuest = null;
                    playerRoster.hasQuest = false;
                    changePanel.Invoke();
                    DataPersistenceManager.instance.SaveGame();
                    return;
                }
                else Debug.Log($"Not enough {playerRoster.currentQuest.itemNeeded} on board");
                return;
            }
        }
    }

    public void AbandonQuest()
    {
        if (playerRoster.hasQuest)
        {
            playerRoster.currentQuest = null;
            playerRoster.hasQuest = false;
            changePanel.Invoke();
            Debug.Log("Quest Abandoned");
            DataPersistenceManager.instance.SaveGame();
        }
    }
}
