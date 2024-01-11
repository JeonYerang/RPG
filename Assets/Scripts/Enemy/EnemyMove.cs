using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Chase,
        isAttacked
    }

    public State state;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Enemy enemy;

    public float moveSpeed;

    int moveDir;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        enemy = gameObject.GetComponent<Enemy>();

        state = State.Idle;
    }
    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                rb.velocity = new Vector2(0, 0);
                if (enemy.target != null)
                {
                    state = State.Chase;
                }
                break;

            case State.Move:
                if (enemy.target != null && state != State.isAttacked)
                {
                    state = State.Chase;
                    break;
                }

                switch (moveDir)
                {
                    case 0:
                        rb.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, 0);
                        break;

                    case 1:
                        rb.velocity = new Vector2(-moveSpeed * Time.fixedDeltaTime, 0);
                        break;
                }
                break;

            case State.Chase:
                if (enemy.target == null)
                {
                    state = State.Idle;
                    break;
                }

                rb.velocity = new Vector2(
                    (enemy.target.transform.position.x - transform.position.x) 
                    * moveSpeed * Time.fixedDeltaTime, 0);

                break;
        }

        animator.SetInteger("State", (int)state);

        if (rb.velocity.x != 0) //스프라이트 좌우뒤집기                  
            spriteRenderer.flipX = rb.velocity.x < 0;

        /*if (rb.velocity.x < 0)
            transform.localScale = new Vector2(-1, 1);
        else if(rb.velocity.x > 0)
            transform.localScale = new Vector2(1, 1);*/
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (state == State.Move)
                state = State.Idle;
            yield return new WaitForSeconds(2f);

            if (state == State.Idle)
            {
                moveDir = UnityEngine.Random.Range(0, 2);
                state = State.Move;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void BeAttacked()
    {
        state = State.isAttacked;
        StartCoroutine(BeAttackedCoroutine());
    }
    IEnumerator BeAttackedCoroutine()
    {
        rb.velocity = (new Vector2(0, 0));
        rb.AddForce(new Vector2(100 * Time.fixedDeltaTime, 0));
        yield return new WaitForSeconds(.5f);
        rb.velocity = (new Vector2(0, 0));
        yield return new WaitForSeconds(.5f);
        state = State.Chase;
    }
}
