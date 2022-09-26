using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//public delegate void CoinChangeUI();

public class MenuScript : MonoBehaviour
{
    private GameObject currentPanel;
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private TextMeshProUGUI dockText, coinText;
    [SerializeField] private SailingManager sailingManager;

    //public event CoinChangeUI OnCoinChange;

    private void OnEnable()
    {
        menuManager.coinChange.AddListener(UpdateCoinText);
    }

    private void OnDisable()
    {
        menuManager.coinChange.RemoveListener(UpdateCoinText);
    }

    private void Start()
    {
        dockText.text = $"\"{playerRoster.currentDock.name}\"";
        UpdateCoinText();
        ToggleCoinText();
        sailingManager.ResetMods();
    }

    public void SwitchPanel(GameObject panelToChange)
    {
        if (currentPanel == panelToChange)
        {
            if (currentPanel.activeSelf.Equals(true))
            {
                currentPanel.SetActive(false);
                currentPanel = null;
            }
            else
            {
                currentPanel.SetActive(true);
                currentPanel = panelToChange;
            }
            ToggleCoinText();
            return;
        }
        if (currentPanel != null) currentPanel.SetActive(false);
        currentPanel = panelToChange;
        currentPanel.SetActive(true);

        ToggleCoinText();
    }

    public void ToggleSellPanel()
    {
        if (sellPanel.activeSelf.Equals(true)) sellPanel.SetActive(false);
        else sellPanel.SetActive(true);
    }

    private void ToggleCoinText()
    {
        if (currentPanel != null)
        {
            coinText.gameObject.SetActive(true);
            UpdateCoinText();
        }
        else
        {
            coinText.gameObject.SetActive(false);
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = $"Coins: {playerRoster.GetCoins()}";
    }

    public void ToNPCRoom()
    {
        SceneManager.LoadScene("NPC Room");
    }

    //public void ToJourney()
    //{
    //    currentPanel.SetActive(false);
    //    currentPanel = journeyPanel;
    //    currentPanel.SetActive(true);
    //}
}
