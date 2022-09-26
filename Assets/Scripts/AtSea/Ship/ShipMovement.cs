using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] SailingManager sailingManager;
    [SerializeField] PlayerRoster playerRoster;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (sailingManager.onNav)
        {
            transform.position += new Vector3(sailingManager.SpeedFactor, 0f) * Time.deltaTime;
            cam.transform.position += new Vector3(sailingManager.SpeedFactor, 0f) * Time.deltaTime;
        }
        playerRoster.ship.SetShipPos(transform.position);
    }
}
