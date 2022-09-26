using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void SailorButtonNotify(Sailor sailor);

public class SailorsTabScript : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private TextMeshProUGUI nameText, navigationText, sailingText, carpentingText, bilgingText, gunningText, treasureHaulingText, swordsmanshipText, brawlingText, medicineText, scoutText, flavorText;
    private Sailor defaultSailor;
    [SerializeField] private GameObject sailorIcon;
    [SerializeField] private GameObject parentToSpawn;
    [SerializeField] private Image sailorPreview;
    private Sailor selectedSailor;

    void Start()
    {
        foreach (Sailor sailor in playerRoster.currentDock.sailorsToSell)
        {
            if (defaultSailor == null) 
            {
                defaultSailor = sailor; 
            }

            var tempSailor = Instantiate(sailorIcon, parentToSpawn.transform);
            var temp = tempSailor.GetComponent<SailorIconButtonHolder>();
            temp.SetSailor (sailor);
            temp.SailorButtonPress += UpdateSailor;
        }

        if (selectedSailor == null) selectedSailor = defaultSailor;
        UpdateText();
        UpdatePreview();
    }

    private void UpdateSailor(Sailor sailor)
    {
        selectedSailor = sailor;
        UpdateText();
        UpdatePreview();
    }

    private void UpdateText()
    {
        nameText.text = selectedSailor.sailorName;
        navigationText.text = $"Navigation: {selectedSailor.sailorAttributes.navigationLevel}";
        sailingText.text = $"Sailing: {selectedSailor.sailorAttributes.sailingLevel}";
        carpentingText.text = $"Carpenting: {selectedSailor.sailorAttributes.carpentingLevel}";
        bilgingText.text = $"Bilging: {selectedSailor.sailorAttributes.bilgingLevel}";
        treasureHaulingText.text = $"Treasure Hauling: {selectedSailor.sailorAttributes.treasureHaulingLevel}";
        swordsmanshipText.text = $"Swordsmanship: {selectedSailor.sailorAttributes.swordmanshipLevel}";
        brawlingText.text = $"Brawling: {selectedSailor.sailorAttributes.brawlingLevel}";
        medicineText.text = $"Medicine: {selectedSailor.sailorAttributes.medicineLevel}";
        scoutText.text = $"Scouting: {selectedSailor.sailorAttributes.scoutLevel}";
        flavorText.text = selectedSailor.flavorText;
    }

    private void UpdatePreview()
    {
        //change the sailor icon here too.
        if (selectedSailor.icon != null)
        sailorPreview.sprite = selectedSailor.icon; /// HERE
    }
}
