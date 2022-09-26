using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{ 
    private Vector3 offset;
    private Vector3 originalPos;
    [SerializeField] private Camera uIDragCamera;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] PlayerRoster playerRoster;

    private Vector3 ShipPos => playerRoster.ship.ShipPos;
    private Vector3 currPos;

    private void Awake()
    {
        uIDragCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("beginDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("onDrag");
        transform.position = MouseWorldPosition() + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.position = originalPos + (ShipPos - currPos);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - MouseWorldPosition();
        originalPos = transform.position;
        currPos = ShipPos;
    }

    private Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = uIDragCamera.WorldToScreenPoint(transform.position).z;
        return uIDragCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draws a 5 unit long red line in front of the object
    //    Gizmos.color = Color.red;
    //    var rayOrigin = Camera.main.transform.position;
    //    var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
    //    Gizmos.DrawRay(rayOrigin, rayDirection);
    //}
}
