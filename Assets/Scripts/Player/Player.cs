using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public string playerName;

    public int level;
    public int exp;

    public int currentHp;
    public int currentMp;

    public enum Occupation
    {
        Warrior,
        Archor,
        Mage
    }
    public Occupation occupation;

    [Serializable]
    public struct Stats
    {
        public int maxHp;
        public int maxMp;
        public int ap;
        public int dp;
    }
    public Stats stats;

    bool isInvincibility;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        currentHp = stats.maxHp;
        currentMp = stats.maxMp;

        isInvincibility = false;
    }

    void Update()
    {
    }

    public void GetDamage(int damage)
    {
        if (!isInvincibility)
        {
            currentHp -= damage;
            if (currentHp < 0)
            {
                currentHp = 0;
                Die();
            }
            UserInfoController.Instance.SetHpBar();
            StartCoroutine(InvincibilityCoroutine(1.5f));
        }
        
    }

    IEnumerator InvincibilityCoroutine(float sec)
    {
        yield return null;
        float startTime = Time.time;

        isInvincibility = true;

        while (Time.time - startTime < sec)
        {
            float alphaValue = 1;

            while (alphaValue >= 0.5f) //점점 투명하게
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, alphaValue);
                alphaValue -= 0.1f;
                yield return new WaitForSeconds(0.02f);
            }

            while (alphaValue <= 1f) //점점 불투명하게
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, alphaValue);
                alphaValue += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        isInvincibility = false;

        yield break;
    }
    void Die()
    {
        //To-Do
    }
}
