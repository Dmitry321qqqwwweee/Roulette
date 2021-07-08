using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseCell : MonoBehaviour
{
    [SerializeField]
    private GameObject content = default;
    private Button contentButton = default;
    private Image contentImage = default;

    [SerializeField]
    private Image chip = default;

    private Field field = default;

    public bool Interactable
    {
        get => contentButton.interactable;
        set => contentButton.interactable = value;
    }

    private int bet = 0;
    public int Bet
    {
        get => bet;
        set
        {
            if (value == 0) { global::Bet.Remove(bet); }
            else { global::Bet.Add(value - bet); }

            bet = value;
            chip.enabled = (value != 0);
            chip.sprite = field.GetChip(value).Sprite;

            if (value != 0) { Select(); }
            else { Deselect(); }
        }
    }

    private int oldBet = 0;

    private void Awake()
    {
        field = GetComponentInParent<Field>();

        contentImage = content.GetComponent<Image>();
        contentButton = content.GetComponent<Button>();
        contentButton.onClick.AddListener(AddBet);
    }

    private void OnDestroy()
    {
        contentButton.onClick.RemoveAllListeners();
    }

    protected void AddBet()
    {
        if (Purse.Coins.TryBuy(field.CurrentChip.Value))
        {
            Sound.PlayBetAdd();
            Bet += field.CurrentChip.Value;
        }
    }

    public abstract void Select();
    public abstract void Deselect();

    public void Revert()
    {
        if (oldBet > 0)
        {
            Bet = oldBet;
        }
    }

    public void Clean()
    {
        oldBet = Bet;
        Bet = 0;
    }
    public void DoubleBet()
    {
        if (Bet > 0) { Bet *= 2; }
    }

    protected void SetSelect(bool _isSelected)
    {
        var color = new Color(1f, 1f, 1f, _isSelected ? .5f : 0f);
        contentImage.color = color;
    }

    //////////////////////////////////////////////////////
    public void Fill()
    {
        content = GetComponentInChildren<Button>().gameObject;
        chip = content.GetComponent<RectTransform>().GetChild(1).GetComponent<Image>();
    }
}
