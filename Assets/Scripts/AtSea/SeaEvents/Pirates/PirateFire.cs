using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PirateFire : MonoBehaviour
{
    [SerializeField] PlayerRoster playerRoster;
    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform fireSpot;

    [SerializeField] private float fireInterval;
    private float fireTimer;

    [SerializeField] private float minRange = 6f;
    [SerializeField] private float maxRange = 30f;

    [SerializeField] private float chanceToHit = 50f;

    private ObjectPool<GameObject> pirateProjectilePool;

    private void Start()
    {
        pirateProjectilePool = new ObjectPool<GameObject>(CreatePoolObject, PoolOnGet, PoolOnRelease, PoolOnDestroy, true, 5, 10);
        fireTimer = fireInterval;
    }

    private GameObject CreatePoolObject()
    {
        var ball = Instantiate(cannonBallPrefab, fireSpot.transform.position, Quaternion.identity, fireSpot.transform);
        return ball;
    }

    private void PoolOnGet(GameObject ball)
    {
        ball.transform.position = fireSpot.transform.position;
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
        var dist = playerRoster.ship.ShipPos.x - transform.position.x;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && dist > minRange && dist < maxRange)
        {
            fireTimer = fireInterval;
            pirateProjectilePool.Get();
        }
    }

    public bool WillItHit()
    {
        var temp = Random.Range(0, 100);
        return temp < chanceToHit;
    }

    public void ReturnToPool(GameObject ball)
    {
        pirateProjectilePool.Release(ball);
    }
}
