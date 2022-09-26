using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalShip : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private TextMeshProUGUI nameText, durabilityText, storageText, sailorsText, navigationText, sailingText, bilgingText, carpentingText, gunningText, scoutingText, flavorText;

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        var info = playerRoster.ship;
        nameText.text = info.shipName;
        //durabilityText.text = $"Durability: {info.currentDurability:#,###.#} / {info.maxDurability:#,###.#} ({info.maxDamagedDurability:#,###.#})";
        //storageText.text = $"Storage: {info.currentStorage:#,###} / {info.maxStorage:#,###}";
        sailorsText.text = $"Sailors: {playerRoster.sailors.Length} / {info.sailorsMax}";
        navigationText.text = "Navigation: " + info.shipStations.navigationStations.ToString();
        sailingText.text = "Sailing: " + info.shipStations.sailingStations.ToString();
        bilgingText.text = "Bilging: " + info.shipStations.bilgingStations.ToString();
        carpentingText.text = "Carpenting: " + info.shipStations.carpentingStations.ToString();
        gunningText.text = "Gunning: " + info.shipStations.gunningStations.ToString();
        scoutingText.text = "Scouting: " + info.shipStations.scoutStations.ToString();
        flavorText.text = info.flavorText;
    }

    private void ConstantTextUpdate()
    {
        var info = playerRoster.ship;
        durabilityText.text = $"Durability: {info.currentDurability:#,###} / {info.maxDurability:#,###} ({info.maxDamagedDurability:#,###})";
        storageText.text = $"Storage: {info.currentStorage:#,##0} / {info.maxStorage:#,###}";
    }

    private void Update()
    {
        ConstantTextUpdate();
    }
}
