using UnityEngine;
using System;

public static class Purse
{
    public static Money Coins = new Money("Coins", 1000);

    public static string GetMoney(int _money, string _separator = ".")
    {
        var money = _money == 0 ? "0" : "";
        for (var i = 0; _money > 0; i++)
        {
            if (i == 3)
            {
                i = 0;
                money += _separator;
            }
            money += _money % 10;
            _money /= 10;
        }
        char[] arr = money.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public class Money
    {
        public Money(string _name, int _defauldMoney)
        {
            NAME = _name;
            DEFAULT_MONEY = _defauldMoney;
        }

        public Action<int> OnChanged = (int _money) => { };

        private string NAME = default;
        private int DEFAULT_MONEY = default;

        public int Value
        {
            get => PlayerPrefs.GetInt(NAME, DEFAULT_MONEY);
            private set
            {
                if (value < 0) { value = 0; }
                PlayerPrefs.SetInt(NAME, value);
                OnChanged(value);
            }
        }

        public void Add(int _money)
        {
            if (_money < 0) { throw new Exception(); }
            Value += _money;
        }

        public void Buy(int _price)
        {
            if ((_price < 0) || (_price > Value)) { throw new Exception(); }
            else { Value -= _price; }
        }

        public bool TryBuy(int _price)
        {
            if ((_price < 0) || (_price > Value)) { return false; }
            Value -= _price;
            return true;
        }
    }
}
