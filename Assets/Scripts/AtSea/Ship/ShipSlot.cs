using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSlot : MonoBehaviour
{
    [SerializeField] private Ship ship;

    public Ship GetShip() { return ship; }
}
