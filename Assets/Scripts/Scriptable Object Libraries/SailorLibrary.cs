using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sailor Library", menuName = "Libraries/Sailor Library")]
public class SailorLibrary : ScriptableObject
{
    public Sailor[] sailors;

    public Sailor FindSailor(string id)
    {
        foreach (Sailor sailor in sailors)
        {
            if (sailor.stringID == id)
                return sailor;
        }
        Debug.Log($"Sailor {id} not found.");
        return null;
    }
}
