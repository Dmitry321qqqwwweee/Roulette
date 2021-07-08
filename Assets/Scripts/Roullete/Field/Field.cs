using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
    [Serializable]
    public class Chip
    {
        [SerializeField]
        private int value = default;
        public int Value => value;
        [SerializeField]
        private Sprite sprite = default;
        public Sprite Sprite => sprite;
    }

    [SerializeField]
    private Chip[] chips = default;
    public int CurrentChipId = 0;
    public Chip CurrentChip => chips[CurrentChipId];

    [SerializeField]
    private Cell[] cells = default;
    [SerializeField]
    private Cells[] cellses = default;

    private bool interactable = true;
    public bool Interactable
    {
        get => interactable;
        set
        {
            interactable = value;
            foreach (var cell in cells) { cell.Interactable = value; }
            foreach (var cell in cellses) { cell.Interactable = value; }
        }
    }

    public void Spin()
    {

    }

    public void Clean(bool saveMoney = true)
    {
        if (saveMoney)
        {
            Purse.Coins.Add(Bet.Get());
        }
        foreach (var cell in cells) { cell.Clean(); }
        foreach (var cell in cellses) { cell.Clean(); }
    }

    public void Double()
    {
        if (Purse.Coins.TryBuy(Bet.Get()))
        {
            foreach (var cell in cells) { cell.DoubleBet(); }
            foreach (var cell in cellses) { cell.DoubleBet(); }
        }
    }
    public void Return()
    {
        foreach (var cell in cells) { cell.Revert(); }
        foreach (var cell in cellses) { cell.Revert(); }
        if (!Purse.Coins.TryBuy(Bet.Get()))
        {
            Clean(false);
        }
    }

    public Chip GetChip(int _bet)
    {
        for (var i = chips.Length - 1; i >= 0; i--)
        {
            if (_bet >= chips[i].Value) { return chips[i]; }
        }
        return chips[0];
    }

    public int Check(Element _element)
    {
        var gain = 0;
        foreach (var cell in cells)
        {
            if ((_element.Number == cell.Element.Number) &&
                (_element.ElementType == cell.Element.ElementType))
            {
                if (cell.Bet > 0)
                {
                    gain += cell.Bet * 36;
                }
            }
        }
        foreach (var c in cellses)
        {
            var cells = c._Cells;
            foreach (var cell in cells)
            {
                if ((_element.Number == cell.Element.Number) &&
                (_element.ElementType == cell.Element.ElementType))
                {
                    if (c.Bet > 0)
                    {
                        gain += c.Bet * (36 / cells.Length);
                    }
                }
            }
        }
        return gain;
    }

    [ContextMenu("Fill")]
    private void Fill()
    {
        foreach (var cell in cells) { cell.Fill(); }
        foreach (var cells in cellses) { cells.Fill(); }
    }
}
