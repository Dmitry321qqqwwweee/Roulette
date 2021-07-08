using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIInfo : Page
{
    [SerializeField]
    private Button exitButton = default;

    protected override void Awake()
    {
        base.Awake();

        exitButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        Close();
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveAllListeners();
    }
}
