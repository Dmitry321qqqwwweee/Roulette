using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusWinUI : Page
{
    [SerializeField]
    private Text winValueText = default;

    public new void Close() => Close(true);
    public override void Close(bool isActive = true)
    {
        base.Close(isActive);

        TimeBonus.Get(TimeBonus.Type.DailyBonus).Get();
    }

    public void OpenWithWinValue(string winText)
    {
        winValueText.text = winText;
        Open();
    }
}
