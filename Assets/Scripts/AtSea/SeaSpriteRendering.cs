using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaSpriteRendering : MonoBehaviour
{
    [SerializeField] Sprite startS, endS, inBetweenS;
    [SerializeField] GameObject player;
    [SerializeField] GameObject normalBG, differentBG;

    private Queue<GameObject> bgQueue = new Queue<GameObject>();
    private Queue<GameObject> bgDifQueue = new Queue<GameObject>();

    GameObject starter, ender, inBetween;

    Vector3 space => new Vector3(startS.texture.width / startS.pixelsPerUnit, 0f, 0f);

    [SerializeField] bool lul = false;

    GameObject lastPiece, midPiece;

    // Start is called before the first frame update
    void Start()
    {
        //float distance = 1000f;
        ////SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        ////sprite.size = new Vector2(40f, sprite.size.y);
        ////var starter = Instantiate(new GameObject("start", typeof(SpriteRenderer)), player.transform.position, Quaternion.identity, transform);
        starter = new GameObject("start", typeof(SpriteRenderer));
        starter.transform.parent = transform;
        starter.transform.position = player.transform.position;
        starter.GetComponent<SpriteRenderer>().sprite = startS;


        //float space = startS.texture.width / startS.pixelsPerUnit;
        //Vector3 spaceVec = new Vector3(space, 0f, 0f);

        //inBetween = new GameObject("between", typeof(SpriteRenderer));
        //inBetween.transform.parent = transform;
        //inBetween.transform.position = player.transform.position + spaceVec;
        //inBetween.GetComponent<SpriteRenderer>().sprite = inBetweenS;

        //var inBeTwo = Instantiate(inBetween, inBetween.transform.position + spaceVec, Quaternion.identity, transform);
        //var inBeThree = Instantiate(inBetween, inBetween.transform.position + spaceVec*2, Quaternion.identity, transform);

        ////var ender = Instantiate(endS, new Vector3(player.transform.position.x + space, player.transform.position.y, player.transform.position.z), Quaternion.identity, transform);
        //ender = new GameObject("ender", typeof(SpriteRenderer));
        //ender.transform.parent = transform;
        //ender.transform.position = inBeThree.transform.position + spaceVec;
        //ender.GetComponent<SpriteRenderer>().sprite = endS;
        QueueInit(normalBG.transform.GetChild(0).gameObject, bgQueue);
        QueueInit(differentBG.transform.GetChild(0).gameObject, bgDifQueue);
        lastPiece = bgQueue.ToArray()[bgQueue.Count-1];
        midPiece = bgQueue.ToArray()[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (lul)
        {
            if (player.transform.position.x > midPiece.transform.position.x)
            {
                var first = bgDifQueue.Dequeue();
                first.transform.position = lastPiece.transform.position + space;
                lastPiece = first;
                bgDifQueue.Enqueue(first);
                midPiece = bgDifQueue.ToArray()[2];
            }
        }
        else
        {
            if (player.transform.position.x > midPiece.transform.position.x)
            {
                var first = bgQueue.Dequeue();
                first.transform.position = lastPiece.transform.position + space;
                lastPiece = first;
                bgQueue.Enqueue(first);
                midPiece = bgQueue.ToArray()[2];
            }
        }
    }

    private void QueueInit(GameObject original, Queue<GameObject> queue)
    {
        List<GameObject> backgrounds = new List<GameObject>();
        original.transform.position = starter.transform.position + space;
        for (int i = 1; i < 5; i++)
        {
            var temp = Instantiate(original, original.transform.position + space * i, Quaternion.identity, transform);
            backgrounds.Add(temp);
        }

        queue.Enqueue(original);
        foreach (GameObject bgs in backgrounds)
        {
            queue.Enqueue(bgs);
        }
    }
}
