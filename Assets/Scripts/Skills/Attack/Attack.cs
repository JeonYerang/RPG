using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D col;
    Player player;

    int ap;

    void Start()
    {
        col = GetComponent<Collider2D>();
        player = GetComponentInParent<Player>();

        this.ap = player.stats.ap;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.GetDamage(this.ap, player);
    }
}
