using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isAttack;

    //List<Enemy> targetEnemys;

    public GameObject attackPrefab;

    Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        isAttack = false;
    }

    void Update()
    {
        animator.SetBool("isAttack", isAttack);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
        

    }
    IEnumerator AttackCoroutine()
    {
        GameObject attackObject = Instantiate(attackPrefab, transform.position, 
            Quaternion.identity,transform);
        isAttack = true;

        yield return new WaitForSeconds(1f);

        Destroy(attackObject);
        isAttack = false;

    }
}
