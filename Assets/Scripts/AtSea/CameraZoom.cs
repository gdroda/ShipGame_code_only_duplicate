using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] SpriteRenderer playerShip;
    [SerializeField] Sprite shipSprite, shipXSprite;
    private const float zoomOne = 6f;
    private const float zoomTwo = 12f;
    private const float ZoomThree = 18f;

    void Start()
    {
        mainCamera.orthographicSize = zoomOne;
    }

    void Update() //SERIALIZE SAILINGMANAGER AND CHECK IF SCOUT ON CROWS NEST FOR MORE ZOOM OUT, ELSE DO NOT ZOOM
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            switch (mainCamera.orthographicSize)
            {
                case zoomTwo:
                    mainCamera.orthographicSize = zoomOne;
                    break;
                case ZoomThree:
                    mainCamera.orthographicSize = zoomTwo;
                    break;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) //backward
        {
            switch (mainCamera.orthographicSize)
            {
                case zoomOne:
                    mainCamera.orthographicSize = zoomTwo;
                    break;
                case zoomTwo:
                    mainCamera.orthographicSize = ZoomThree;
                    break;
            }
        }

        if (mainCamera.orthographicSize == zoomOne)
        {
            playerShip.sprite = shipXSprite;
        }
        else playerShip.sprite = shipSprite;
    }
}
