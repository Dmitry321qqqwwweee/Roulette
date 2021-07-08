using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusButton : MonoBehaviour
{
    private DailyBonusUIController dailiBonus = default;
    [SerializeField]
    private Text timerText = default;

    private void SetActive(bool _value)
    {
        dailiBonus.SwitchStateDailyImage(_value);
        timerText.enabled = !_value;
    }


    private void Awake()
    {
        dailiBonus = GetComponent<DailyBonusUIController>();

        TimeBonus.Get(TimeBonus.Type.DailyBonus).OnSetAvailible += SetActive;

        TimeBonus.Get(TimeBonus.Type.DailyBonus).OnSetTimeRemain += () =>
        { timerText.text = TimeBonus.Get(TimeBonus.Type.DailyBonus).GetRest(); };
    }

    private void Start()
    {
        SetActive(TimeBonus.Get(TimeBonus.Type.DailyBonus).IsAvailible);
    }

    private void OnDestroy()
    {
        TimeBonus.Get(TimeBonus.Type.DailyBonus).OnSetAvailible -= SetActive;
    }
}
