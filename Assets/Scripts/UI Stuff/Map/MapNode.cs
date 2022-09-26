using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapNode : MonoBehaviour
{
    [SerializeField] private Dock dock;
    [SerializeField] private MapManager mapManager;
    [SerializeField] public List<MapNode> neighboringNodes;
    public UnityEvent<MapNode> nodeClicked;

    private void OnEnable()
    {
        if (nodeClicked == null) nodeClicked = new UnityEvent<MapNode>();
    }

    public Dock GetDock() => dock;

    public void Clicked()
    {
        nodeClicked?.Invoke(this);
    }
}
