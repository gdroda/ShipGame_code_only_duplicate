using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship Library", menuName = "Libraries/Ship Library")]
public class ShipLibrary : ScriptableObject
{
    public Ship[] ships;
    //public Dictionary<string, Ship> shipDict = new Dictionary<string, Ship>();

    //private void Awake()
    //{
    //    foreach (Ship ship in ships)
    //    {
    //        shipDict.Add(ship.shipName, ship);
    //    }
    //}

    public Ship FindShip(string id)
    {
        foreach (Ship ship in ships)
        {
            if (ship.stringID == id)
                return ship;
        }
        Debug.Log($"Ship {id} not found.");
        return null;
    }
}
