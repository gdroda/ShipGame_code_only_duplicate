using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] PlayerRoster playerRoster;
    [SerializeField] SailingManager sailingManager;
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform fireSpot;

    [SerializeField] private float fireInterval;
    private float fireTimer;

    [SerializeField] private float chanceToHit;

    private ObjectPool<GameObject> playerProjectilePool;

    private void Start()
    {
        playerProjectilePool = new ObjectPool<GameObject>(CreatePoolObject, PoolOnGet, PoolOnRelease, PoolOnDestroy, true, 5, 10);
        fireTimer = fireInterval;
    }

    private GameObject CreatePoolObject()
    {
        var ball = Instantiate(cannonBallPrefab, new Vector3(transform.position.x, transform.position.y - 3.5f), Quaternion.identity, fireSpot.transform);
        return ball;
    }

    private void PoolOnGet(GameObject ball)
    {
        ball.transform.position = fireSpot.position;
        ball.SetActive(true);
    }

    private void PoolOnRelease(GameObject ball)
    {
        ball.SetActive(false);
    }

    private void PoolOnDestroy(GameObject ball)
    {
        Destroy(ball);
    }

    private void Update()
    {
        if (sailingManager.gunningMod > 7f) fireInterval = 2f;
        else if (sailingManager.gunningMod > 4f) fireInterval = 4f;
        else if (sailingManager.gunningMod > 1f) fireInterval = 6f;

        var dist = playerRoster.ship.ShipPos.x - transform.position.x; //DO DIST TOO

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && sailingManager.gunningMod > 0f)
        {
            fireTimer = fireInterval;
            playerProjectilePool.Get();
        }
    }

    public bool WillItHit()
    {
        var temp = Random.Range(0, 100);
        return temp < chanceToHit;
    }

    public void ReturnToPool(GameObject ball)
    {
        playerProjectilePool.Release(ball);
    }
}
