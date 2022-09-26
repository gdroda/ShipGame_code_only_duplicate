using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIconButtonHolder : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    private Ship ship;

    public event ShipButtonNotify ShipButtonPress;

    public void SetShip(Ship _ship)
    {
        ship = _ship;
    }

    public void ButtonPressed()
    {
        Debug.Log(ship.shipName);
        ShipButtonPress?.Invoke(ship);
    }
}
