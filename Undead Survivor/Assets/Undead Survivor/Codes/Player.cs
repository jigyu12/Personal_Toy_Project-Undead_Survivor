using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            rigid.velocity = Vector2.zero;
            return;
        }
        
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // Normalized Æ÷ÇÔ
    }

    void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        
        anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
           return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy.speed == 2f)
        {
            GameManager.instance.health -= Time.deltaTime * 10;
        }
        else if(enemy.speed == 2.1f)
        {
            GameManager.instance.health -= Time.deltaTime * 12;
        }
        else if (enemy.speed == 2.3f)
        {
            GameManager.instance.health -= Time.deltaTime * 14;
        }
        else if (enemy.speed == 2.6f)
        {
            GameManager.instance.health -= Time.deltaTime * 17;
        }
        else if (enemy.speed == 3f)
        {
            GameManager.instance.health -= Time.deltaTime * 20;
        }

        if (GameManager.instance.health < 0)
        {
            for(int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}