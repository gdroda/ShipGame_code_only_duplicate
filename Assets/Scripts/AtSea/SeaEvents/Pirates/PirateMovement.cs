using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMovement : MonoBehaviour
{
    [SerializeField] private PlayerRoster playerRoster;
    private float offset = 5f;
    private float slowTimer;
    [SerializeField] private float pirateSpeed;
    [SerializeField] private float slowedPirateSpeed;

    private void Update()
    {
        if (transform.position.x < (playerRoster.ship.ShipPos.x - offset))
        {
            if (slowTimer > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerRoster.ship.ShipPos, slowedPirateSpeed * Time.deltaTime);
                slowTimer -= Time.deltaTime;
            }
            else transform.position = Vector2.MoveTowards(transform.position, playerRoster.ship.ShipPos, pirateSpeed * Time.deltaTime);
        }//else pause game (?) open ui for options regarding pirate catch
    }

    public void ResetTimer()
    {
        slowTimer = 2f;
    }
}
