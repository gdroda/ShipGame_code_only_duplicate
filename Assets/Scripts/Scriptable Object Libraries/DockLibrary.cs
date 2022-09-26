using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dock Library", menuName = "Libraries/Dock Library")]
public class DockLibrary : ScriptableObject
{
    public Dock[] dockLib;

    public Dock FindDock(string id)
    {
        foreach (Dock dock in dockLib)
        {
            if (dock.stringID == id)
                return dock;
        }
        Debug.Log($"Dock {id} not found.");
        return null;
    }
}
