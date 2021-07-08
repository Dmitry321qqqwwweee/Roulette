using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimeBonus : MonoBehaviour
{
    [SerializeField]
    private string availibleText = default;
    [SerializeField]
    private string format = "HMS";
    [SerializeField]
    private string separator = ":";

    [SerializeField]
    private TimeBonus.Type type = default;

    [SerializeField]
    private Text text = default;
    private Button button = default;

    private TimeBonus.Bonus bonus = default;

    private void Awake()
    {
        button = GetComponent<Button>();
        bonus = TimeBonus.Get(type);
        bonus.OnSetTimeRemain += OnSetTimeRemain;
    }

    private void Start()
    {
        OnSetTimeRemain();
    }

    private void OnSetTimeRemain()
    {
        SetText(bonus.IsAvailible ? availibleText : bonus.GetRest(format, separator));
        button.interactable = bonus.IsAvailible;
    }

    private void SetText(string _text) =>
        text.text = _text;
}
