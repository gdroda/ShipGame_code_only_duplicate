using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using System.Linq;

public class Charting : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Button chartButton;
    [SerializeField] private Button goButton;
    [SerializeField] private GameObject infoPanel;

    private List<MapNode> tempChartNodes = new List<MapNode>();
    private List<MapNode> finalChartedNodes = new List<MapNode>();
    private List<MapNode> mapNodeObjects;

    private GameObject player;
    private MapNode playerMapNode;

    private MapNode selectedNode;
    private Dock selectedDock;

    public UnityEvent<List<MapNode>> chartPass;

    private int nodesTraveled;

    private void OnEnable()
    {
        if (chartPass == null) chartPass = new UnityEvent<List<MapNode>>();
    }

    private void Start()
    {
        goButton.onClick.AddListener(GOBUTTON); //go button functionality
        goButton.interactable = false;
        mapManager.isCharting = false;
        chartButton.onClick.AddListener(ChartCourse);
        player = GameObject.FindGameObjectWithTag("PlayerMapNode");
        playerMapNode = player.GetComponent<MapNode>();

        mapNodeObjects = FindObjectsOfType<MapNode>().ToList();

        nodesTraveled = 0;

        if (SceneManager.GetActiveScene().name == "AtSea") GOBUTTON();
        else PlayerNeighbors();
    }

    private void GOBUTTON()
    {
        mapManager.isCharting = false;
        goButton.interactable = false;
        finalChartedNodes.Clear();
        finalChartedNodes.AddRange(tempChartNodes); //CURRENT ERROR IS CAUSED HERE CAUSE AT START THERE IS NO TEMPCHART NODE, REASSESS THE WHOLE CONNECTIVITY
        //tempChartNodes.Clear();
        chartPass?.Invoke(finalChartedNodes);
        PlayerNeighbors();
        DataPersistenceManager.instance.SaveGame();
    }

    public void OpenInfo(MapNode node)
    {
        selectedNode = node;
        //clickedNode = node.gameObject;
        //nodePos = node.gameObject.transform.localPosition;
        selectedDock = selectedNode.GetDock();
        infoPanel.SetActive(true);
        UpdateInfo();
    }

    public void NodePassed()
    {
        nodesTraveled++;
        //finalChartedNodes.Remove(finalChartedNodes[0]);
        playerMapNode.neighboringNodes.Remove(playerMapNode.neighboringNodes[0]);
        PlayerNeighbors();
        playerRoster.currentDock = finalChartedNodes.Last().GetDock();
    }

    private void UpdateInfo()
    {
        if (selectedDock != null)
        {
            infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedDock.dockName;
            //infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance: {(int)Distance}km";
        }
        else
        {
            infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Node Test";
            //infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Distance: {(int)Distance}km";
        }
    }

    private void ChartCourse()
    {
        if (!mapManager.isCharting)
        {
            if (playerMapNode.neighboringNodes.Contains(selectedNode))
            {
                mapManager.isCharting = true;
                goButton.interactable = true;
                if (tempChartNodes.Contains(selectedNode))
                {
                    tempChartNodes.Remove(selectedNode);
                    //DO LINE STUFF - REMOVE LINE
                    if (tempChartNodes.Count <= 0)
                    {
                        mapManager.isCharting = false;
                        goButton.interactable = false;
                    }
                }
                else
                {
                    tempChartNodes.Add(selectedNode);
                    //DO LINE STUFF - ADD LINE
                }
            }
        }
        else
        {
            if (tempChartNodes.Last().neighboringNodes.Contains(selectedNode) || tempChartNodes.Last().Equals(selectedNode))
            {
                if (tempChartNodes.Contains(selectedNode))
                {
                    tempChartNodes.Remove(selectedNode);
                    // REMOVE LINE
                    if (tempChartNodes.Count <= 0)
                    {
                        mapManager.isCharting = false;
                        goButton.interactable = false;
                    }
                }
                else
                {
                    tempChartNodes.Add(selectedNode);
                    // ADD LINE
                }
            }
        }
        //foreach (MapNode node in chartedNodes)
        //{
        //    Debug.Log(node.name);
        //}
    }

    private void PlayerNeighbors()
    {
        playerMapNode.neighboringNodes.Clear(); //keep?
        if (SceneManager.GetActiveScene().name == "AtSea")
        {
            if (nodesTraveled == 0)
            {
                foreach (MapNode node in mapNodeObjects)
                {
                    if (node.GetDock() == playerRoster.currentDock)
                    {
                        playerMapNode.neighboringNodes.Add(node);
                        
                    }
                }
            }
            if (finalChartedNodes.Count > 0)
                playerMapNode.neighboringNodes.Add(finalChartedNodes[0]);
        }
        else if (SceneManager.GetActiveScene().name == "Dock")
        {
            foreach (MapNode node in mapNodeObjects)
            {
                if (node.GetDock() == playerRoster.currentDock)
                {
                    playerMapNode.neighboringNodes.AddRange(node.neighboringNodes);
                }
            }
        }
        playerMapNode.neighboringNodes = playerMapNode.neighboringNodes.Distinct().ToList();
    }

    #region Save/Load
    public void LoadData(GameData data)
    {
        Debug.Log("maps loading");
        if (data.chartedNodes.Length > 0)
        {
            this.finalChartedNodes.Clear();
            foreach (string nodeID in data.chartedNodes) // 2 options
            {
                foreach (MapNode node in mapNodeObjects) // 5 options
                {
                    if (node.GetDock() != null)
                    {
                        if (node.GetDock().stringID == nodeID)
                        {
                            this.finalChartedNodes.Add(node);
                        }
                    }
                }
            }
        }
        //chartPass?.Invoke(finalChartedNodes);

    }

    public void SaveData(ref GameData data)
    {
        if (this.finalChartedNodes.Count > 0)
        {
            List<string> nodeIds = new List<string>();
            foreach (MapNode node in this.finalChartedNodes)
            {
                nodeIds.Add(node.GetComponent<MapNode>().GetDock().stringID);
            }
            data.chartedNodes = nodeIds.ToArray();
        }
        Debug.Log("maps saved");
    }
    #endregion
}
