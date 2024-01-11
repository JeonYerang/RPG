using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoController : MonoBehaviour
{
    public static UserInfoController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public TextMeshProUGUI userName;
    public TextMeshProUGUI level;
    public HpBar hpBar;
    public MpBar mpBar;

    private void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null;

        SetName();
        SetLevel();
        SetHpBar();
        SetMpBar();
    }

    public void SetName()
    {
        userName.text = GameManager.Instance.player.playerName;
    }

    public void SetLevel()
    {
        level.text = $"Lv.{GameManager.Instance.player.level}";
    }

    public void SetHpBar()
    {
        hpBar.SetHpBar();
    }

    public void SetMpBar()
    {
        mpBar.SetMpBar();
    }
}
