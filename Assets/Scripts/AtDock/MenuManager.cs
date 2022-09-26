using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MenuManager")]
public class MenuManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent coinChange;

    private void OnEnable()
    {
        if (coinChange == null) coinChange = new UnityEvent();
    }

    public void OnCoinChange()
    {
        coinChange?.Invoke();
    }
}
