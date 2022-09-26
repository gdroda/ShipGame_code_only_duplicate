using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalMain : MonoBehaviour
{
    [SerializeField] GameObject journal, questPanel, shipPanel;
    [SerializeField] Image questBtn, shipBtn, sailorBtn, commodityBtn; //CHECK DIMBUTTONS FUNCTION
    private GameObject currentPanel;

    private void Start()
    {
        PanelsOff();
        currentPanel = questPanel;
        currentPanel.SetActive(true);
        DimButtons();
        journal.SetActive(false);
    }

    public void ToggleJournal() => journal.SetActive(journal.activeSelf == true ? false : true);

    private void PanelsOff()
    {
        questPanel.SetActive(false);
        shipPanel.SetActive(false);
    }

    public void OpenQuestPanel()
    {
        if (currentPanel != questPanel)
        {
            currentPanel.SetActive(false);
            questPanel.SetActive(true);
            currentPanel = questPanel;
            DimButtons();
        }
        else return;
    }

    public void OpenShipPanel()
    {
        if (currentPanel != shipPanel)
        {
            currentPanel.SetActive(false);
            shipPanel.SetActive(true);
            currentPanel = shipPanel;
            DimButtons();
        }
        else return;
    }

    private void DimButtons() //LATER CHANGE SPRITE INSTEAD OF CHANGING BUTTON COLORS
    {
        if (currentPanel == questPanel)
        {
            questBtn.color = Color.white;
            shipBtn.color = Color.grey;
            sailorBtn.color = Color.grey;
            commodityBtn.color = Color.grey;
        }
        else if (currentPanel == shipPanel)
        {
            questBtn.color = Color.grey;
            shipBtn.color = Color.white;
            sailorBtn.color = Color.grey;
            commodityBtn.color = Color.grey;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleJournal();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPanel == questPanel) OpenShipPanel();
            else if (currentPanel == shipPanel) OpenQuestPanel();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentPanel == questPanel) OpenShipPanel();
            else if (currentPanel == shipPanel) OpenQuestPanel();
        }
    }
}
