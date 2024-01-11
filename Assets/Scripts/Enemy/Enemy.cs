using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName;

    public int level;
    public int currentHp;

    public Player target = null;
    EnemyInfo info;

    [Serializable]
    public struct Stats
    {
        public int maxHp;
        public int ap;
        public int dp;
    }

    public Stats stats;

    EnemyMove move;

    private void Awake()
    {
        info = gameObject.GetComponentInChildren<EnemyInfo>();

        currentHp = stats.maxHp;
    }
    
    public abstract void Attack();

    public void Hit(Player player)
    {
        player.GetDamage(stats.ap);
    }

    public void GetDamage(int damage, Player player)
    {
        target = player;

        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
            Die();
        }

        info.SetHpBar();

        move.BeAttacked();
    }

    

    public void Die()
    {
        Destroy(gameObject);
    }
}
