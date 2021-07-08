using UnityEngine;
using System;

public static class Bet
{
    private const string BET = "Bet";
    private const int DEFAULT_VALUE = 0;

    public static Action<int> OnBetChanged = (int _bet) => { };

    private static int _bet = 0;
    private static int bet
    {
        get => _bet;
        set
        {
            if (value < 0) { value = 0; }
            _bet = value;
            OnBetChanged(value);
        }
    }

    public static int Get() => bet;
    public static void Add(int _value)
    {
        if (_value < 0) { throw new Exception(); }
        bet += _value;
    }
    public static void Remove(int _value)
    {
        if (_value < 0) { throw new Exception(); }
        bet -= _value;
    }
    public static void Clean() => bet = 0;
}
