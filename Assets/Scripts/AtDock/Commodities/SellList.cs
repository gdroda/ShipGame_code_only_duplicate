using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellList : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private GameObject commPrefab;

    private void OnEnable()
    {
        PopulatePanel();
    }

    private void OnDisable()
    {
        EmptyChildren();
    }

    private void PopulatePanel()
    {
        foreach (CommodityOnShip comm in playerRoster.ship.commoditiesOnShip)
        {
            var commPref = Instantiate(commPrefab, transform);
            var temp = commPref.GetComponent<SellCommoditySlot>();
            temp.SetCommodity(comm.commodity, comm.amount, comm);
        }
    }

    private void EmptyChildren()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
