using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetExpBar()
    {
        int currentExp = GameManager.Instance.player.exp;
        int maxExp = 1;
        float amount = (float)currentExp / (float)maxExp;

        slider.value = amount;
        text.text = $"{currentExp}/{maxExp} ({System.Math.Round(amount, 2)}%)";
    }
}
