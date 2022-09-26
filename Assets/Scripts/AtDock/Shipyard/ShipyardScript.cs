using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void ShipButtonNotify(Ship ship);

public class ShipyardScript : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private TextMeshProUGUI titleText, speedText, durabilityText, storageText, maxSailorsText, sailingText, carpentingText, bilgingText, gunningText, scoutText, restText, flavorText;
    private Ship defaultShip;
    [SerializeField] private GameObject shipIcon;
    [SerializeField] private GameObject parentToSpawn;
    [SerializeField] private Image shipPreview;
    private Ship selectedShip;

    private float RepairPrice => Mathf.Ceil((playerRoster.ship.maxDurability - playerRoster.ship.currentDurability) * playerRoster.currentDock.repairMod);

    void Start()
    {
        foreach (Ship ship in playerRoster.currentDock.shipsToSell)
        {
            if (defaultShip == null) 
            {
                defaultShip = ship; 
            }

            var tempShip = Instantiate(shipIcon, parentToSpawn.transform);
            var temp = tempShip.GetComponent<ShipIconButtonHolder>();
            temp.SetShip(ship);
            temp.ShipButtonPress += UpdateShip;
        }

        if (selectedShip == null) selectedShip = defaultShip;
        UpdateText();
    }

    private void UpdateShip(Ship ship)
    {
        selectedShip = ship;
        UpdateText();
        UpdatePreview();
    }

    private void UpdateText()
    {
        titleText.text = selectedShip.shipName;
        speedText.text = $"Speed: {selectedShip.speed}";
        durabilityText.text = $"Durability: {selectedShip.maxDurability}";
        storageText.text = $"Storage: {selectedShip.maxStorage}";
        maxSailorsText.text = $"Max Sailors: {selectedShip.sailorsMax}";
        sailingText.text = $"Sailing: {selectedShip.shipStations.sailingStations}";
        carpentingText.text = $"Carpenting: {selectedShip.shipStations.carpentingStations}";
        bilgingText.text = $"Bilging: {selectedShip.shipStations.bilgingStations}";
        gunningText.text = $"Gunning: {selectedShip.shipStations.gunningStations}";
        scoutText.text = $"Scouting: {selectedShip.shipStations.scoutStations}";
        restText.text = $"Resting: {selectedShip.shipStations.restStations}";
        flavorText.text = selectedShip.flavorText;
    }

    private void UpdatePreview()
    {
        //change the ship icon here too.
        if (selectedShip.shipModel != null)
        shipPreview.sprite = selectedShip.shipModel;
    }

    public void RepairShip()
    {
        if (playerRoster.ship.currentDurability < playerRoster.ship.maxDurability)
        {
            if (playerRoster.GetCoins() >= RepairPrice)
            {
                Debug.Log($"Paid {RepairPrice} to restore {playerRoster.ship.maxDurability - playerRoster.ship.currentDurability} points of durability");
                playerRoster.GiveCoins(-RepairPrice);
                playerRoster.ship.currentDurability = playerRoster.ship.maxDurability;
                playerRoster.ship.maxDamagedDurability = playerRoster.ship.maxDurability;
                menuManager.OnCoinChange();
            }
            else Debug.Log("Not enough coins..."); //else message not enough coins
        } 
        else Debug.Log("Durability is already full!"); //else message its durability is full
    }
}
