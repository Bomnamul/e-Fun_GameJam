﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RunController : MonoBehaviour
{
    /* 기본적인 조작 (점프, 이단 점프)
     * speed, score, life, jumpCount
     * GetDamage(), Movement()
     */

    public LayerMask layerMask;
    public float speed;

    Vector3 forward;
    Rigidbody2D rbody;
    BoxCollider2D col;
    int score = 0;
    [SerializeField]
    int life = 100;
    [SerializeField]
    int jumpCount = 2;
    Vector3 jumpArrivePoint;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        forward = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // input.space 로 ready to run 필요
        //Movement();
    }

    private void FixedUpdate()
    {
        Movement();

    }

    public void GetDamage(int damage)
    {
        if (life - damage <= 0)
        {
            life = 0;
            Debug.Log("Game Over");
            // GameOver 추가 필요
        }

        life -= damage;
    }

    void CheckOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.52f, layerMask);

        if (hit)
        {
            jumpCount = 2;
        }

    }

    void Movement()
    {
        CheckOnGround();
        rbody.velocity = new Vector2(forward.x, rbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount != 0)
        {
            jumpCount--;
            jumpArrivePoint = transform.position;

            rbody.velocity = new Vector2(rbody.velocity.x, 25f);

        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacles")
        {
            GetDamage(10);
        }
    }
}
