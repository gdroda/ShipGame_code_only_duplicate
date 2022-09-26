using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StartSailingFromSea : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Button goButton;
    [SerializeField] private Button chartButton;

    public UnityEvent<Vector3> linePoint;

    private GameObject clickedNode;
    private Vector3 nodePos;
    private MapNode mapNode;
    private Dock dock;
    private GameObject lastNode;
    private List<GameObject> tempChartedNodes = new List<GameObject>();

    private float Distance => Vector2.Distance(playerPos.transform.localPosition, nodePos);

    private void OnEnable()
    {
        if (linePoint == null) linePoint = new UnityEvent<Vector3>();
    }

    public void GetChart(List<MapNode> chartedNodes)
    {
        playerRoster.destinationDock = chartedNodes.Last().GetDock();
        //playerRoster.currentDistanceToTravel = CalculateDistance(); //REASSESS THIS //divide to make longer maybe?
        DataPersistenceManager.instance.SaveGame();

        if (SceneManager.GetActiveScene().name == "Dock")
        {
            SceneManager.LoadScene("AtSea");
        }
    }

    //////public void ChangeDestination()
    //////{
    //////    mapManager.chartedNodes.Clear();
    //////    mapManager.chartedNodes.AddRange(tempChartedNodes);
    //////    var dest = mapManager.chartedNodes.Last();
    //////    mapManager.isCharting = false;
    //////    goButton.interactable = false;
    //////    mapManager.OnCourseSet();

    //////    if (SceneManager.GetActiveScene().name == "Dock")
    //////    {
    //////        playerRoster.destinationDock = dest.GetComponent<MapNode>().GetDock();
    //////        playerRoster.currentDistanceToTravel = CalculateDistance(); //divide to make longer maybe?
    //////        mapManager.OnDestinationSet();
    //////        tempChartedNodes.Clear();
    //////        DataPersistenceManager.instance.SaveGame();
    //////        SceneManager.LoadScene("AtSea");
    //////        return;
    //////    }

    //////    if (playerRoster.destinationDock != dock)
    //////    {
    //////        playerRoster.destinationDock = dest.GetComponent<MapNode>().GetDock();
    //////        playerRoster.currentDistanceToTravel = CalculateDistance(); //divide to make longer maybe?
    //////        mapManager.OnDestinationSet();
    //////        //infoPanel.SetActive(false);
    //////        tempChartedNodes.Clear();
    //////        DataPersistenceManager.instance.SaveGame();
    //////    }
    //////}

    //////public void OpenInfo(MapNode node)
    //////{
    //////    mapNode = node;
    //////    clickedNode = node.gameObject;
    //////    nodePos = node.gameObject.transform.localPosition;
    //////    dock = mapNode.GetDock();
    //////    infoPanel.SetActive(true);
    //////    UpdateInfo();
    //////}

    public Dock GetDock() => dock;

    //////private void UpdateInfo()
    //////{
    //////    if (dock != null)
    //////    {
    //////        infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dock.dockName;
    //////        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance: {(int)Distance}km";
    //////    }
    //////    else
    //////    {
    //////        infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Node Test";
    //////        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance: {(int)Distance}km";
    //////    }
    //////}

    private void Update()
    {
        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance: {(int)Distance}km";
    }

    //////private void ChartCourse()
    //////{
    //////    if (!mapManager.isCharting) //check if charting to change neighboring base
    //////    {
    //////        if (mapNode.neighboringNodes.Contains(mapManager.currentNode)) //check if neighboring
    //////        {
    //////            mapManager.isCharting = true;
    //////            goButton.interactable = true;
    //////            if (tempChartedNodes.Contains(clickedNode)) //check if already charted
    //////            {
    //////                tempChartedNodes.Remove(clickedNode); //if already charted then unchart // FIX THIS AND UNCHART UP TO THAT POINT
    //////                linePoint.Invoke(nodePos);
    //////                if (tempChartedNodes.Count.Equals(0))//if charted nodes empty, turn isCharting to false
    //////                {
    //////                    mapManager.isCharting = false;
    //////                    goButton.interactable = false;
    //////                } 
    //////            }
    //////            else
    //////            {
    //////                tempChartedNodes.Add(clickedNode); //if not then chart it
    //////                linePoint.Invoke(nodePos);
    //////            }
    //////        }
    //////    }
    //////    else
    //////    {
    //////        if (mapNode.neighboringNodes.Contains(lastNode)) //check if neighboring
    //////        {
    //////            tempChartedNodes.Add(clickedNode); //if not then chart it
    //////            linePoint.Invoke(nodePos);
    //////        }
    //////        else if (tempChartedNodes.Contains(clickedNode)) //check if already charted
    //////        {
    //////            tempChartedNodes.Remove(clickedNode); //if already charted then unchart // FIX THIS AND UNCHART UP TO THAT POINT
    //////            linePoint.Invoke(nodePos);
    //////            if (tempChartedNodes.Count.Equals(0))//if charted nodes empty, turn isCharting to false
    //////            {
    //////                mapManager.isCharting = false;
    //////                goButton.interactable = false;
    //////            }
                    
    //////        }
    //////    }
    //////    if (mapManager.isCharting) lastNode = tempChartedNodes.Last();
    //////}

    private float CalculateDistance()
    {
        float accuDist = Vector2.Distance(mapManager.currentNode.transform.localPosition, tempChartedNodes[0].transform.localPosition);
        for (int i = 0; i < tempChartedNodes.Count()-1; i++)
        {
            accuDist += Vector2.Distance(mapManager.chartedNodes[i].transform.localPosition, tempChartedNodes[i + 1].transform.localPosition);
        }
        accuDist -= Vector2.Distance(playerPos.transform.localPosition, mapManager.currentNode.transform.localPosition);
        return accuDist;
    }
}
