using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoulette : Page
{
    [SerializeField]
    private Button spinButton = default;
    [SerializeField]
    private Button cleanButton = default;
    [SerializeField]
    private Button doubleButton = default;
    [SerializeField]
    private Button repeatButton = default;

    [SerializeField]
    private Text paidText = default;

    [SerializeField]
    private TableMove.Table table = default;
    [SerializeField]
    private Roullete.WinRollete winRollete = default;
    private Field field = default;

    [SerializeField]
    private History history = default;

    [SerializeField]
    private Button increaseBetButton = default;
    [SerializeField]
    private Slot.UISlot slot = default;

    private int lastWin = 0;

    private bool _isSpinning = false;
    private bool isSpinning
    {
        get => _isSpinning;
        set
        {
            _isSpinning = value;
            spinButton.interactable = !value;
            cleanButton.interactable = !value;
            doubleButton.interactable = !value;
            repeatButton.interactable = !value;
            field.Interactable = !value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        Screen.orientation = ScreenOrientation.Portrait;

        field = GetComponentInChildren<Field>();

        spinButton.onClick.AddListener(SpinButtonClick);
        cleanButton.onClick.AddListener(CleanButtonClick);
        doubleButton.onClick.AddListener(DoubleButtonClick);
        repeatButton.onClick.AddListener(RepeatButtonClick);

        table.MoveToFortuneAction += () => { Sound.PlayWheelSound(); isSpinning = true; };
        table.TableEndMove += () => { isSpinning = false; };

        winRollete.WinValue += (Element _element) => { history.Add(_element); };
        winRollete.WinValue += (Element _element) =>
        {
            var gain = field.Check(_element);
            paidText.text = Purse.GetMoney(gain) + "";
            if (gain > 0) { Sound.PlayWinRoulette(); }
            if (gain > 0) { Purse.Coins.Add(gain); }
            lastWin = gain;
            increaseBetButton.gameObject.SetActive(gain >= 500);
            field.Clean(false);
        };

        Bet.OnBetChanged += (int _bet) =>
        {
            cleanButton.gameObject.SetActive(_bet > 0);
            repeatButton.gameObject.SetActive(_bet == 0);
        };

        increaseBetButton.onClick.AddListener(OpenSlot);
        increaseBetButton.onClick.AddListener(() => { Sound.PlayIncreaseBet(); });
    }

    private void OpenSlot()
    {
        increaseBetButton.gameObject.SetActive(false);
        slot.Open(lastWin);
    }

    private void Start()
    {
        cleanButton.gameObject.SetActive(Bet.Get() > 0);
        repeatButton.gameObject.SetActive(Bet.Get() == 0);
        Open();
    }

    private void OnDestroy()
    {
        spinButton.onClick.RemoveAllListeners();
        cleanButton.onClick.RemoveAllListeners();
        doubleButton.onClick.RemoveAllListeners();
        repeatButton.onClick.RemoveAllListeners();

        Purse.Coins.Add(Bet.Get());
        Bet.Clean();
    }

    private void SpinButtonClick()
    {
        Sound.PlaySpinButton();
        field.Spin();
        table.MoveToFortuneAction();
    }

    private void CleanButtonClick()
    {
        if (Bet.Get() > 0)
        {
            Sound.PlayChipsSound();
        }
        field.Clean();
    }
    

    private void DoubleButtonClick()
    {
        int bet = Bet.Get();
        field.Double();

        if (Bet.Get() != bet)
        {
            Sound.PlayChipsSound();
        }
    }
        

    private void RepeatButtonClick()
    {
        field.Return();
        if (Bet.Get() > 0)
        {
            Sound.PlayChipsSound();
        }
    }
        
}
