using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private float coinsToGet;

    private void OnMouseDown()
    {
        playerRoster.GiveCoins(coinsToGet);
        Debug.Log(playerRoster.GetCoins());
        Destroy(gameObject);
    }
}
