using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

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
    [SerializeField]
    float jumpPow;
    bool onRun = false;
    CinemachineImpulseSource impulse;

    private void Awake()
    {
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
        if (life - damage <= 0)
        {
            life = 0;
            Debug.Log("Game Over");
            GameManager.instance.SetGameState(GameManager.state.Lose);
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

            rbody.velocity = new Vector2(rbody.velocity.x, jumpPow);

        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacles") // 방해물과 trigger 발생 시 속도 느려짐, 체력 깎음
        {
            GetDamage(10);
        }

        if (collision.transform.tag == "CompanyPoint") // 회사에 도착 할 경우 정지 후 Mini 게임 전환
        {
            onRun = false;
            GameManager.instance.SetGameState(GameManager.state.Mini);
        }

        if(collision.transform.tag == "Jem")
        {
            score += 10;
            collision.gameObject.SetActive(false);
        }
    }
}
