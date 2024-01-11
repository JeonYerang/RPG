using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    Collider2D col;
    Player player;

    int ap;
    float enhancedRate;

    void Start()
    {
        col = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();

        this.ap = (int)(player.stats.ap * enhancedRate);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.GetDamage(this.ap, player);
    }
}
