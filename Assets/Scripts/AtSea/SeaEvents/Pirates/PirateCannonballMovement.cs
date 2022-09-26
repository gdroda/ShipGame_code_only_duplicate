using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateCannonballMovement : MonoBehaviour
{
    private GameObject pirateShip;
    private PirateFire pirateFire;
    [SerializeField] private PlayerRoster playerRoster;

    private float playerXOffset;
    private bool willHit;

    private Rigidbody2D rb;
    private float gravity;
    private GameObject targetObj;

    void Awake()
    {
        pirateShip = GameObject.FindGameObjectWithTag("Pirate");
        targetObj = GameObject.FindGameObjectWithTag("PlayerCannonHitSpot");
        pirateFire = pirateShip.GetComponent<PirateFire>();

        rb = GetComponent<Rigidbody2D>();
        gravity = Physics.gravity.magnitude;
    }

    private void OnEnable()
    {
        playerXOffset = Random.Range(-2.5f, 2.5f);
        willHit = pirateFire.WillItHit();

        Vector3 target = targetObj.transform.position;

        Vector3 planarTarget = new Vector3(target.x + playerXOffset, 0, target.z);
        Vector3 planarPosition = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = transform.position.y - target.y;
        
        float angle = distance > 20f ? 45f * Mathf.Deg2Rad : 30f * Mathf.Deg2Rad;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

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
            if (collision.CompareTag("PlayerCannonHitSpot"))
            {
                Debug.Log("it HIT");
                playerRoster.ship.currentDurability -= 100f;
                playerRoster.ship.maxDamagedDurability -= 50f;
                pirateFire.ReturnToPool(gameObject);
            }
        }
        else if (!collision.CompareTag("PlayerCannonHitSpot") && !collision.CompareTag("PirateCannonHitSpot"))
        {
            Debug.Log("it missed");
            pirateFire.ReturnToPool(gameObject);
        }
    }
}
