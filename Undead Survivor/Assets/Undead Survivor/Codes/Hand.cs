using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3 (0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0,0,-35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;

        if(isLeft)
        {            
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else if (GameManager.instance.player.scanner.nearestTarget)
        {
            spriter.flipX = false;
            spriter.sortingOrder = 6;

            Vector3 targetPos = GameManager.instance.player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            transform.localRotation = Quaternion.FromToRotation(Vector3.right, dir);
            bool isRotA = transform.localRotation.eulerAngles.z > 90 && transform.localRotation.eulerAngles.z < 270; // 각도는 0 ~ 360 인거 같음
            bool isRotB = transform.localRotation.eulerAngles.z < -90 && transform.localRotation.eulerAngles.z > -270; // 굳이 필요 없는 코드 인듯 ?
            spriter.flipY = isRotA || isRotB;
        }
        else
        {
            spriter.flipY = false;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            //transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            //spriter.sortingOrder = isReverse ? 6 : 4;
            spriter.sortingOrder = 6;
        }
    }
}