using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chip : MonoBehaviour
{
    [SerializeField]
    private int id = default;

    private Toggle toggle = default;

    [SerializeField]
    private Field field = default;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(SetStatus);
    }

    private void Start()
    {
        SetStatus(toggle.isOn);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }

    private void SetStatus(bool _value)
    {
        if (_value)
        {
            field.CurrentChipId = id;
        }
    }
}
