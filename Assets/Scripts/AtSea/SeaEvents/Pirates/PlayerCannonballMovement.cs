using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCannonballMovement : MonoBehaviour
{
    private GameObject playerShip;
    private PlayerFire playerFire;
    private PirateMovement pirateMovement;
    [SerializeField] private PlayerRoster playerRoster;

    private float pirateXOffset;
    private bool willHit;

    private Rigidbody2D rb;
    private float gravity;
    private GameObject targetObj;

    void Awake()
    {
        targetObj = GameObject.FindGameObjectWithTag("PirateCannonHitSpot");
        playerShip = GameObject.Find("Ship Shell");
        playerFire = playerShip.GetComponent<PlayerFire>();
        if (GameObject.FindGameObjectsWithTag("Pirate").Length > 0)
            pirateMovement = GameObject.FindGameObjectWithTag("Pirate").GetComponent<PirateMovement>();
        else Debug.Log("No pirate found! - PlayerCannonballMovement");

        rb = GetComponent<Rigidbody2D>();
        gravity = Physics.gravity.magnitude;
    }

    private void OnEnable()
    {
        pirateXOffset = Random.Range(-2.5f, 2.5f);
        willHit = playerFire.WillItHit();

        Vector3 target = targetObj.transform.position;

        Vector3 planarTarget = new Vector3(target.x + pirateXOffset, 0, target.z);
        Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = transform.position.y - target.y;

        float angle = distance > 20f ? 45f * Mathf.Deg2Rad : 30f * Mathf.Deg2Rad;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), -initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPosition);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        //rb.velocity = finalVelocity;

        // Alternative way:
        rb.AddForce(finalVelocity * rb.mass, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (willHit)
        {
            if (collision.CompareTag("PirateCannonHitSpot"))
            {
                pirateMovement.ResetTimer();
                playerFire.ReturnToPool(gameObject);
            }
        }
        else if (!collision.CompareTag("PirateCannonHitSpot") && !collision.CompareTag("PlayerCannonHitSpot"))
        {
            playerFire.ReturnToPool(gameObject);
        }
    }
}