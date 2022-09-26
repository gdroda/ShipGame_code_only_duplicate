using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private MapManager mapManager;
    private List<Vector3> points = new List<Vector3>();
    private Vector3 PlayerPosImg => mapManager.currentNode.transform.localPosition;

    private void OnEnable()
    {
        mapManager.CourseSet.AddListener(ResetPoints);        
    }

    private void OnDisable()
    {
        mapManager.CourseSet.RemoveListener(ResetPoints);
    }

    private void Start()
    {
        points.Clear();
        points.Add(new Vector3(PlayerPosImg.x, PlayerPosImg.y, 21));
        lr.positionCount = points.Count;
    }

    public void AddPoints(Vector3 pointo)
    {
        var point = new Vector3(pointo.x, pointo.y, 21);
        if (points.Contains(point))
        {
            points.Remove(point);
        }else points.Add(point);
        DrawLine();
    }

    public void ResetPoints()
    {
        points.Clear();
        points.Add(new Vector3(PlayerPosImg.x, PlayerPosImg.y, 21));
        lr.positionCount = points.Count;
        DrawLine();
    }

    private void DrawLine()
    {
        lr.positionCount = points.Count;
        var pointsu = points.ToArray();
        for (int i = 0; i < pointsu.Length; i++)
        {
            lr.SetPosition(i, pointsu[i]);
        }
        //lr.SetPositions(points.ToArray());
    }

    private void Update()
    {
        points[0] = new (PlayerPosImg.x, PlayerPosImg.y, 21);
        DrawLine();
    }
}
