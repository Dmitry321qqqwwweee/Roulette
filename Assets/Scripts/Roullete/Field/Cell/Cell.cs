using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : BaseCell
{
    [SerializeField]
    private Element element = default;
    public Element Element => element;

    public override void Select()
    {
        SetSelect(true);
    }

    public override void Deselect()
    {
        SetSelect(false);
    }
}
