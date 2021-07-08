using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [System.Serializable]
    private class Cell
    {
        private Element element = default;
        public Element Element
        {
            get => element;
            set
            {
                element = value;
                image.enabled = value != null;
                text.enabled = value != null;
                if (value != null)
                {
                    var number = "";
                    Sprite sprite = default;
                    switch (value.ElementType)
                    {
                        case Element.Type.Zero:
                            number = "0";
                            sprite = parent.zeroSprite;
                            break;
                        case Element.Type.DoubleZero:
                            number = "00";
                            sprite = parent.zeroSprite;
                            break;
                        case Element.Type.Red:
                            number = value.Number + "";
                            sprite = parent.redSprite;
                            break;
                        case Element.Type.Black:
                            number = value.Number + "";
                            sprite = parent.blackSprite;
                            break;
                    }
                    image.sprite = sprite;
                    text.text = number;
                }
            }
        }
        [SerializeField]
        private Image image = default;
        [SerializeField]
        private Text text = default;
        private History parent = default;

        public void Init(History _history)
        {
            parent = _history;
            Element = null;
        }
    }

    [SerializeField]
    private Sprite blackSprite = default;
    [SerializeField]
    private Sprite redSprite = default;
    [SerializeField]
    private Sprite zeroSprite = default;

    [SerializeField]
    private Cell[] cells = default;

    private void Awake()
    {
        foreach (var cell in cells)
        {
            cell.Init(this);
        }
    }

    public void Add(Element element)
    {
        for (var i = cells.Length - 1; i > 0; i--)
        {
            cells[i].Element = cells[i - 1].Element;
        }
        cells[0].Element = element;
    }
}
