using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapMovement : MonoBehaviour
{
    //    [SerializeField] private PlayerRoster playerRoster;
    //    [SerializeField] private MapManager mapManager;
    [SerializeField] private SailingManager sailingManager;
    //    [SerializeField] private GameObject playerPos;
    private Vector3 targetLocalPos;
    [SerializeField] private GameObject mapPanel;
    //    private MapNode playerMapNode;

    //    [SerializeField] private MapNode[] dotNodeLibrary;

    private GameObject player;
    private List<MapNode> chartedList;

    public UnityEvent nodePassed;

    public void GetChart(List<MapNode> chartedNodes)
    {
        chartedList = chartedNodes;
        targetLocalPos = chartedNodes[0].gameObject.transform.localPosition;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerMapNode");
    }

    private void Update()
    {
        if (sailingManager.onNav)
            player.transform.localPosition = Vector2.MoveTowards(player.transform.localPosition, targetLocalPos, sailingManager.SpeedFactor * Time.deltaTime);

        if (player.transform.localPosition == targetLocalPos) //MAKE IT SO NODE PASSED HAPPENS ONCE, ALL CHARTED NODES ARE REMOVED INSTEAD OF 1, FIX NODEPASSED NEIGHBORS
        {
            nodePassed?.Invoke();
            StartCoroutine(ChartUpdate());
            //chartedNodes.Remove(chartedNodes[0]);
            //if (chartedNodes.Count > 0)
            //targetLocalPos = chartedNodes[0].gameObject.transform.localPosition;
        }

        if (Input.GetKeyDown(KeyCode.M) && (SceneManager.GetActiveScene().name == "AtSea"))
        {
            ToggleMap();
        }
    }

    IEnumerator ChartUpdate()
    {
        Debug.Log("Coroutine ding");
        chartedList.Remove(chartedList[0]);
        if (chartedList.Count > 0)
            targetLocalPos = chartedList[0].gameObject.transform.localPosition;
        yield return new WaitForSeconds(1f);
        StopCoroutine(ChartUpdate());
    }

    //    private void OnEnable()
    //    {
    //        mapManager.DestinationSet.AddListener(SetPointToMove);
    //        mapManager.CourseSet.AddListener(Neighbors);
    //    }

    //    private void OnDisable()
    //    {
    //        mapManager.DestinationSet.RemoveListener(SetPointToMove);
    //        mapManager.CourseSet.RemoveListener(Neighbors);
    //    }

    //    private void Start()
    //    {


    //        foreach (MapNode mNode in dotNodeLibrary) //need to fix starting distance to travel.
    //        {
    //            var dock = mNode.GetDock();
    //            if (playerRoster.currentDock == dock)
    //            {
    //                playerPos.transform.localPosition = mNode.transform.localPosition;
    //                mapManager.currentNode = playerPos;

    //                playerMapNode = playerPos.GetComponent<MapNode>();
    //                playerMapNode.neighboringNodes.Add(mNode.gameObject);

    //                if (SceneManager.GetActiveScene().name == "Dock")
    //                {
    //                    foreach (GameObject node in mNode.neighboringNodes)
    //                    {
    //                        node.GetComponent<MapNode>().neighboringNodes.Add(playerPos);
    //                    }
    //                }
    //                else break;

    //            }
    //        }

    //        if (mapManager.chartedNodes.Count > 0) playerMapNode.neighboringNodes.Add(mapManager.chartedNodes[0]);

    //        //foreach (GameObject node in playerMapNode.neighboringNodes)
    //        //{
    //        //    node.GetComponent<MapNode>().neighboringNodes.Add(playerPos);
    //        //}

    //        //foreach (var item in mapManager.chartedNodes)
    //        //{
    //        //    Debug.Log(item);
    //        //}
    //        Debug.Log("before");

    //        if (SceneManager.GetActiveScene().name != "AtSea") mapPanel.SetActive(true);
    //        else mapPanel.SetActive(false);
    //        Debug.Log("after");
    //    }

    //    private void Neighbors()
    //    {
    //        var temp = mapManager.chartedNodes[0].GetComponent<MapNode>();
    //        if (!temp.neighboringNodes.Contains(playerPos)) temp.neighboringNodes.Add(playerPos);
    //        if (!playerMapNode.neighboringNodes.Contains(mapManager.chartedNodes[0])) playerMapNode.neighboringNodes.Add(mapManager.chartedNodes[0]);

    //    }

    //    private void ChangeNodeNeighbors(GameObject node, int mode)
    //    {
    //        var nodeMN = node.GetComponent<MapNode>();

    //        if (mode == 1)
    //            playerMapNode.neighboringNodes.Add(nodeMN.gameObject);

    //        if (mode == 1)
    //        {
    //            foreach (GameObject neighbor in nodeMN.neighboringNodes)
    //            {
    //                if (mode == 1)
    //                    if (playerMapNode.neighboringNodes.Contains(neighbor))
    //                    {
    //                        neighbor.GetComponent<MapNode>().neighboringNodes.Remove(playerPos);
    //                    }
    //            }
    //            playerMapNode.neighboringNodes.Clear();
    //        }
    //        else if (mode == 2)
    //        {
    //            playerMapNode.neighboringNodes.Add(nodeMN.gameObject);
    //            nodeMN.neighboringNodes.Add(playerPos);
    //            //if (!playerMapNode.neighboringNodes.Contains(neighbor))
    //            //{
    //            //    neighbor.GetComponent<MapNode>().neighboringNodes.Add(playerPos);
    //            //}
    //        }


    //    }

    //    private void SetPointToMove()
    //    {
    //        targetLocalPos = mapManager.chartedNodes[0].transform.localPosition;
    //    }

    //    void Update()
    //    {
    //        //if (targetPos != null)
    //        if (sailingManager.onNav)
    //        playerPos.transform.localPosition = Vector2.MoveTowards(playerPos.transform.localPosition, targetLocalPos, sailingManager.SpeedFactor * Time.deltaTime);

    //        //if ((Vector2)playerPos.transform.localPosition == targetLocalPos) //NODE REACHED, MAKE IT BETTER
    //        //{
    //        //    //ChangeNodeNeighbors(mapManager.chartedNodes[0], 1);

    //        //    //mapManager.chartedNodes.Remove(mapManager.chartedNodes[0]);

    //        //    //if (mapManager.chartedNodes.Count != 0)
    //        //    //ChangeNodeNeighbors(mapManager.chartedNodes[0], 2);

    //        //    //mapManager.OnLineReset();
    //        //    SetPointToMove();
    //        //}

    //        
    //    }

    public void ToggleMap() => mapPanel.SetActive(mapPanel.activeSelf.Equals(true) ? false : true);

}
