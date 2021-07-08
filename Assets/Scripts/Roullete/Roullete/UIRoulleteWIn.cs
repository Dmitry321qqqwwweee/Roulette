using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoulleteWIn : Page
{
    private Image circleBackGround = default;

    [SerializeField]
    private Text winValue = default;

    [SerializeField]
    private Sprite redSprite = default;

    [SerializeField]
    private Sprite blackSprite = default;

    [SerializeField]
    private Sprite greenSprite = default;

    protected override void Awake()
    {
        base.Awake();

        circleBackGround = GetComponent<Image>();

    }

    public void OpenRoulleteValue(Element inputCell)
    {
        switch (inputCell.ElementType)
        {
            case Element.Type.Black:
                circleBackGround.sprite = blackSprite;
                break;
            case Element.Type.Red:
                circleBackGround.sprite = redSprite;
                break;
            case Element.Type.Zero:
                circleBackGround.sprite = greenSprite;
                break;
            case Element.Type.DoubleZero:
                circleBackGround.sprite = greenSprite;
                break;

        }

        winValue.text = inputCell.Number.ToString();


        Open();
    }

}
