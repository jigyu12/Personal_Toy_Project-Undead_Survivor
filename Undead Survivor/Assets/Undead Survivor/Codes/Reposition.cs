using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        

        switch(transform.tag) 
        {
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                else
                {
                    transform.Translate(new Vector3(dirX * 40, dirY * 40, 0));
                }
                break;
            case "Enemy":
                if(coll.enabled) 
                {
                    Vector3 dist  = playerPos - myPos;
                    
                    if(Mathf.Abs(dist.x) > Mathf.Abs(dist.y))
                    {
                        Vector3 ran = new Vector3(Random.Range(-2, 2), Random.Range(-6, 6), 0);
                        transform.Translate(dist * 2 + ran);
                    }
                    else if(Mathf.Abs(dist.x) < Mathf.Abs(dist.y))
                    {
                        Vector3 ran = new Vector3(Random.Range(-6, 6), Random.Range(-2, 2), 0);
                        transform.Translate(dist * 2 + ran);
                    }
                    else
                    {
                        Vector3 ran = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
                        transform.Translate(dist * 2 + ran);
                    }
                }
                break;
        }
    }
}