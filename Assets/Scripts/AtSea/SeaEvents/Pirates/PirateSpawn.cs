using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawn : MonoBehaviour
{
    [SerializeField] PlayerRoster playerRoster;
    [SerializeField] GameObject piratePrefab;
    private List<GameObject> piratesSpawned = new List<GameObject>();
    private float spawnPoint;
    private float spawnOffset = 40f;

    private float MinSpawn => playerRoster.currentDistanceToTravel * 0.3f;
    private float MaxSpawn => playerRoster.currentDistanceToTravel * 0.7f;

    private void Start()
    {
        piratesSpawned.Clear();
        SpawnPirate();
    }

    private void SpawnPirate()
    {
        spawnPoint = Random.Range(MinSpawn, MaxSpawn);

        var pir = Instantiate(piratePrefab, new Vector3(spawnPoint, 0f), Quaternion.identity, transform);
        pir.SetActive(false);
        piratesSpawned.Add(pir);
    }

    private void LateUpdate() //LATE UPDATE CAUSE IT WAS CALCULATING OLD SHIP POS AND SPAWNING, CHANGE LATER DEPENDING ON SPAWN CODE
    {
        if (playerRoster.ship.ShipPos.x > spawnPoint + spawnOffset)
        {
            piratesSpawned[0].SetActive(true);
        }
    }
}
