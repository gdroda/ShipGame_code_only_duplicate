using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailorIconButtonHolder : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    private Sailor sailor;

    public event SailorButtonNotify SailorButtonPress;

    public void SetSailor(Sailor _sailor)
    {
        sailor = _sailor;
    }

    public void ButtonPressed()
    {
        Debug.Log(sailor.sailorName);
        SailorButtonPress?.Invoke(sailor);
    }
}
