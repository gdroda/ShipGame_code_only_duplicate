using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[CreateAssetMenu(fileName = "MapManager")]
public class MapManager : ScriptableObject
{
    [System.NonSerialized] public UnityEvent<GameObject> DestinationChange;
    [System.NonSerialized] public UnityEvent DestinationSet;
    [System.NonSerialized] public UnityEvent DestinationSetExtras;
    [System.NonSerialized] public UnityEvent CourseSet;

    public List<GameObject> chartedNodes = new List<GameObject>();
    //public List<GameObject> chartedNodes;
    public bool isCharting = false;
    public GameObject currentNode;

    private void Awake()
    {
        currentNode = GameObject.FindGameObjectWithTag("PlayerMapNode");
    }

    private void OnEnable()
    {
        //chartedNodes.Clear();
        if (DestinationChange == null) DestinationChange = new UnityEvent<GameObject>();
        if (DestinationSet == null) DestinationSet = new UnityEvent();
        if (DestinationSetExtras == null) DestinationSetExtras = new UnityEvent();
        if (CourseSet == null) CourseSet = new UnityEvent();
    }

    public void OnDestinationChange(GameObject dockDot)
    {
        DestinationChange?.Invoke(dockDot);
    }

    public void OnDestinationSet()
    {
        DestinationSet?.Invoke();
        DestinationSetExtras?.Invoke();
    }

    public void OnCourseSet()
    {
        CourseSet?.Invoke();
    }

    ////public void LoadData(GameData data)
    ////{
    ////    if (data.chartedNodes.Length > 0)
    ////    {
    ////        this.chartedNodes.Clear();
    ////        var objectArray = FindObjectsOfType<MapNode>();
    ////        List<MapNode> objectList = objectArray.ToList<MapNode>();
    ////        Debug.Log(objectList.Count);
    ////        Debug.Log(data.chartedNodes.Length);
    ////        foreach (string nodeID in data.chartedNodes) // 2 options
    ////        {
    ////            foreach (MapNode node in objectList) // 5 options
    ////            {
    ////                if (node.GetDock() != null)
    ////                {
    ////                    if (node.GetDock().stringID == nodeID)
    ////                    {
    ////                        this.chartedNodes.Add(node.gameObject);
    ////                    }
    ////                }
    ////            }
    ////        }
    ////    }
    ////}

    ////public void SaveData(ref GameData data)
    ////{
    ////    if (this.chartedNodes.Count > 0 )
    ////    {
    ////        List<string> nodeIds = new List<string>();
    ////        foreach (GameObject node in this.chartedNodes)
    ////        {
    ////            nodeIds.Add(node.GetComponent<MapNode>().GetDock().stringID);
    ////        }
    ////        data.chartedNodes = nodeIds.ToArray();
    ////    }
    ////}
}