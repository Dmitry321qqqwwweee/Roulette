using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowMoney : MonoBehaviour
{
    private Text text = default;

    private Purse.Money money = default;

    private void Awake()
    {
        text = GetComponent<Text>();

        money = Purse.Coins;
        money.OnChanged += SetText;
    }

    private void Start()
    {
        SetText(money.Value);
    }

    private void SetText(int _money)
    {
        text.text = Purse.GetMoney(_money);
    }
}
