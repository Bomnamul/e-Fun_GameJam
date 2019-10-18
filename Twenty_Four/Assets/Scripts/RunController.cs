using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class RunController : MonoBehaviour
{
    /* 기본적인 조작 (점프, 이단 점프)
     * speed, score, healthPoint, jumpCount
     * GetDamage(), Movement()
     */

    public LayerMask layerMask;
    public float speed;

    Vector3 forward;
    Animator anim;
    Rigidbody2D rbody;
    BoxCollider2D col;
    CinemachineImpulseSource impulse;
    int score = 0;
    [SerializeField]
    int healthPoint = 100;
    [SerializeField]
    int jumpCount = 2;
    [SerializeField]
    float jumpPow;
    bool onRun = false;
    

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        impulse = GetComponent<CinemachineImpulseSource>();

    }

    void Start()
    {
        forward = new Vector3(speed, 0, 0);
    }

    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.Ready)
        {
            onRun = true;
            anim.SetBool("OnRun", true);
            GameManager.instance.SetGameState(GameManager.state.Run);
            impulse.GenerateImpulse();
        }
        else if (onRun && GameManager.instance.gameStatus == GameManager.state.Run)
        {
            Movement();
        }
    }

    public void GetDamage(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            healthPoint = 0;
            Debug.Log("Game Over");
            GameManager.instance.SetGameState(GameManager.state.Lose);
            return;
        }

        healthPoint -= damage;
        anim.SetTrigger("OnHurt");
    }

    void CheckOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, layerMask);

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

            rbody.velocity = new Vector2(rbody.velocity.x, jumpPow);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacles") // 방해물과 trigger 발생 시 속도 느려짐, 체력 깎음
        {
            GetDamage(10);
            StartCoroutine(SpeedDown());
            UIManager.instance.SetUIHealthRemain(healthPoint);
        }

        if (collision.transform.tag == "CompanyPoint") // 회사에 도착 할 경우 impulse 생성, 파티클 생성
        {
            impulse.GenerateImpulse();
        }        

        if(collision.transform.tag == "Jem")
        {
            score += 10;
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.tag == "MinigamePoint")  // 부장과만나면 정지 후 mini 게임 전환
        {
            onRun = false;
            GameManager.instance.SetGameState(GameManager.state.Mini);
        }
    }

    IEnumerator SpeedDown()
    {
        forward.x = 0;

        while (forward.x < speed)
        {
            forward.x++;
            yield return new WaitForSeconds(0.1f);
        }

        forward.x = speed;
    }
}
