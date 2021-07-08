using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cells : BaseCell
{
    [SerializeField]
    private Cell[] cells = default;
    public Cell[] _Cells => cells;

    public override void Select()
    {
        SetSelect(true);
        foreach(var cell in cells) { cell.Select(); }
    }

    public override void Deselect()
    {
        SetSelect(false);
        foreach (var cell in cells) { cell.Deselect(); }
    }
}
