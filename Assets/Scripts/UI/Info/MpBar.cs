using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MpBar : MonoBehaviour
{
    Slider slider;
    public TextMeshProUGUI text;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetMpBar()
    {
        int currentMp = GameManager.Instance.player.currentMp;
        int maxMp = GameManager.Instance.player.stats.maxMp;
        float amount = (float)currentMp / (float)maxMp;

        slider.value = amount;
        text.text = $"{currentMp}/{maxMp}";
    }
}
