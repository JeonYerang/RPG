using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetHpBar()
    {
        int currentHp = GameManager.Instance.player.currentHp;
        int maxHp = GameManager.Instance.player.stats.maxHp;
        float amount = (float)currentHp / (float)maxHp;

        slider.value = amount;
        text.text = $"{currentHp}/{maxHp}";
    }
}
