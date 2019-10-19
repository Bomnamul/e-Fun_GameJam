using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;
using Assets.Pixelation.Scripts;

public class RunController : MonoBehaviour
{
    /* 기본적인 조작 (점프, 이단 점프)
     * speed, score, healthPoint, jumpCount
     * GetDamage(), Movement()
     */

    public LayerMask layerMask;
    public float speed;
    public ParticleSystem smokefx;
    public bool onRun = false;

    Animator anim;
    Rigidbody2D rbody;
    BoxCollider2D col;
    CinemachineImpulseSource impulse;
    int score = 0;
    [SerializeField]
    int jumpCount = 2;
    [SerializeField]
    float jumpPow;
    int i;
    bool canJump = true;
    

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        impulse = GetComponent<CinemachineImpulseSource>();

    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.gameStatus == GameManager.state.Ready)
        {
            GameManager.instance.SetGameState(GameManager.state.Run);
            onRun = true;
            anim.SetBool("OnRun", true);
            smokefx.Play();
        }
        else if (onRun && GameManager.instance.gameStatus == GameManager.state.Run)
        {
            Movement();

        }

        UIManager.instance.scoreTxt.text = "업무 평가" + "\n" + score.ToString();

    }

    private void GetDamage(int damage)
    {
        if (GameManager.instance.playerHP - damage <= 0)
        {
            anim.SetTrigger("OnHurt");
            anim.SetBool("OnDeath", true);
            return;
        }

        anim.SetTrigger("OnHurt");
    }

    void CheckOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, layerMask);

        if (hit)
        {
            jumpCount = 2;
            if (smokefx.isStopped)
                smokefx.Play();
        }

    }

    void Movement()
    {
        CheckOnGround();
        rbody.velocity = new Vector2(speed, rbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount != 0 && canJump)
        {
            jumpCount--;

            rbody.velocity = new Vector2(rbody.velocity.x,  jumpPow);
            smokefx.Stop();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacles") // 방해물과 trigger 발생 시 속도 느려짐, 체력 깎음
        {
            GetDamage(10);
            GameManager.instance.GetDamage(10);
            StartCoroutine(SpeedDown());
        }

        if (collision.transform.tag == "CompanyPoint") // 회사에 도착 할 경우 impulse 생성, 파티클 생성
        {           
            smokefx.gameObject.SetActive(false);
        }

        if (collision.transform.tag == "Door")
        {
            StartCoroutine(OpenKick());
        }

        if(collision.transform.tag == "Jem")
        {
            score += 10;
            GameManager.instance.GetHeal(1);
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.tag == "MinigamePoint")  // 부장과만나면 정지 후 mini 게임 전환
        {
            StartCoroutine(MinigamePoint());                       
        }
    }

    IEnumerator MinigamePoint()
    {
        canJump = false;
        speed = 10f;
        anim.SetBool("OnWalk", true);
        anim.SetBool("OnRun", false);

        yield return new WaitForSeconds(1f);
        onRun = false;
        speed = 0f;
        anim.SetBool("OnWalk", false);
        while(Camera.main.GetComponent<Pixelation>().BlockCount > 64)
        {
            Camera.main.GetComponent<Pixelation>().BlockCount -= 5;
            yield return null;
        }
        //GameManager.instance.SetGameState(GameManager.state.MiniReady);
        SceneMgr.instance.LoadScene(6);
    }

    IEnumerator SpeedDown()
    {
        smokefx.Stop();
        float originSpeed = speed;
        speed = 0;

        while (speed < originSpeed)
        {
            speed++;
            yield return new WaitForSeconds(0.1f);
        }

        speed = originSpeed;
        smokefx.Play();
    }

    IEnumerator OpenKick()
    {
        onRun = false;
        rbody.velocity = Vector2.zero;
        impulse.GenerateImpulse();
        anim.SetTrigger("OnKick");
        yield return new WaitForSeconds(0.5f);
        onRun = true;
        anim.SetBool("OnRun", true);
    }

}
