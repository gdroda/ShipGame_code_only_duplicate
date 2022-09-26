using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject treasurePrefab;
    [SerializeField] private PlayerRoster playerRoster;
    private float MinSpawn => playerRoster.currentDistanceToTravel * 0.1f;
    private float MaxSpawn => playerRoster.currentDistanceToTravel * 0.9f;
    private int TreasuresToSpawn => Mathf.CeilToInt(playerRoster.currentDistanceToTravel / 200f);

    void Start()
    {
        List<float> spawnPoints = new List<float>();
        for (int i = 0; i < TreasuresToSpawn; i++)
        {
            var point = Random.Range(MinSpawn, MaxSpawn);
            //add logic for if they're too close!
            spawnPoints.Add(point);
        }
        List<GameObject> treasures = new List<GameObject>(); //probably not needed, just in case
        foreach (float spawn in spawnPoints)
        {
            var tr = Instantiate(treasurePrefab, new Vector3(spawn, 0f), Quaternion.identity, transform);
            treasures.Add(tr);
        }
    }
}
