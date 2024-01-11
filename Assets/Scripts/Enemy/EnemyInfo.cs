using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    Enemy enemy;
    Slider slider;
    public TextMeshProUGUI enemyName;

    private void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
        slider = gameObject.GetComponentInChildren<Slider>();

        SetHpBar();
        SetNameTag();
    }
    public void SetHpBar()
    {
        int currentHp = enemy.currentHp;
        int maxHp = enemy.stats.maxHp;
        float amount = (float)currentHp / (float)maxHp;

        slider.value = amount;
    }
    public void SetNameTag()
    {
        enemyName.text = $"{enemy.enemyName}";
    }
}
