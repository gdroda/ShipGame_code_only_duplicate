using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateInteractiveCanvas : MonoBehaviour
{
    [SerializeField] PlayerRoster playerRoster;
    [SerializeField] GameObject sailorBarPrefab;

    void OnEnable()
    {
        PopulatePanel();
    }

    private void PopulatePanel()
    {
        foreach (Sailor sailor in playerRoster.sailors)
        {
            var sailorBarPref = Instantiate(sailorBarPrefab, transform);
            var temp = sailorBarPref.GetComponent<SailorSlot>();
            temp.SetSailor(sailor);
        }
    }
}
