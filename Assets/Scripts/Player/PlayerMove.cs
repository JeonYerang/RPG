using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    enum State
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Fall = 3,
        Sit = 4
    }

    State state;

    //SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    public float moveSpeed;
    public float jumpPower;

    bool onPlat = false; //내려갈 수 있는 발판 위인지
    bool isGrounded; // 땅에 닿아있는 상태인지
    private void Awake()
    {
        //spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        state = State.Idle;
    }

    /*private void FixedUpdate()
    {
        
    }*/
    void Update()
    {
        animator.SetInteger("State", (int)state);

        //입력
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            HorizontalMove();
        }
        if (Input.GetButtonDown("Jump")) //점프
        {
            Jump();
        }
        if (Input.GetAxisRaw("Vertical") < 0) //앉기
        {
            if(state == State.Idle || state == State.Walk)
                state = State.Sit;
        }

        //상태
        switch (state)
        {
            case State.Idle:
                if (!isGrounded) //떨어짐
                {
                    state = State.Fall;
                    break;
                }
                break;

            case State.Walk:
                if(Input.GetAxisRaw("Horizontal") == 0) //멈춤
                {
                    rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y);
                    state = State.Idle;
                    break;
                }

                if (!isGrounded) //떨어짐
                {
                    state = State.Fall;
                    break;
                }
                break;

            case State.Jump:
                if (rb.velocity.y <= 0) //떨어지는 상태로 전환
                {
                    StartCoroutine(ReturnIdleLayer(0.2f));
                    state = State.Fall;
                    break;
                }
                break;

            case State.Fall:
                if (isGrounded && rb.velocity.y >= -0.0001)
                    state = State.Idle;
                break;

            case State.Sit:
                rb.velocity = new Vector2(0, 0);
                if (Input.GetAxisRaw("Vertical") >= 0) //일어서기
                {
                    state = State.Idle;
                    break;
                }
                    
                if (Input.GetButtonDown("Jump") && !onPlat)
                {
                    state = State.Idle;
                    break;
                }
                if (Input.GetButtonDown("Jump") && onPlat) //떨어지기
                {
                    gameObject.layer = LayerMask.NameToLayer("Throughing");
                    StartCoroutine(ReturnIdleLayer(0.5f));
                    StartCoroutine(ChangeFallState(0.05f));
                    break;
                }
                break;
        }

        if (Input.GetAxis("Horizontal") < 0) //좌우뒤집기
            transform.localScale = new Vector2(-1, 1);
        else if (Input.GetAxis("Horizontal") > 0)
            transform.localScale = new Vector2(1, 1);

        //if (Input.GetAxis("Horizontal") != 0) //스프라이트 좌우뒤집기
        //    spriteRenderer.flipX = Input.GetAxis("Horizontal") < 0;
    }

    public void HorizontalMove()
    {
        float speed = moveSpeed;

        if (state == State.Idle)
        {
            state = State.Walk;
        }
        else if (state == State.Fall || state == State.Jump) //느리게 움직임
        {
            speed *= 0.5f;
        }
        else if (state == State.Sit) //움직이지 않음
        {
            return;
        }

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed
            * Time.fixedDeltaTime, rb.velocity.y); //좌우이동, y는 그대로
    }
    public void Jump()
    {
        if(state != State.Jump && state != State.Fall && state != State.Sit)
        {
            rb.AddForce(new Vector2(0, jumpPower * Time.fixedDeltaTime),
            ForceMode2D.Impulse);

            state = State.Jump;

            gameObject.layer = LayerMask.NameToLayer("Throughing");
        }
    }

    IEnumerator ChangeFallState(float sec)
    {
        yield return new WaitForSeconds(sec);
        state = State.Fall;
        yield break;
    }
    IEnumerator ReturnIdleLayer(float sec)
    {
        yield return new WaitForSeconds(sec);
        gameObject.layer = LayerMask.NameToLayer("Player");
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;

        if (collision.collider.tag == "Plat")
        {
            onPlat = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        onPlat = false;
    }
}

