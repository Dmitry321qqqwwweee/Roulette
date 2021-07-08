using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class UISlot : Page
    {
        [SerializeField]
        private Button backButton = default;
        [SerializeField]
        private Button infoButton = default;

        //[SerializeField]
        //private BetPanel betPanel = default;

        [SerializeField]
        private Text betText = default;

        [SerializeField]
        private Text winText = default;

        [SerializeField]
        private Button spinButton = default;

        private Slot slot = default;

        [SerializeField]
        private UIInfo info = default;

        private bool _isSpin = false;
        private bool isSpin
        {
            get => _isSpin;
            set
            {
                _isSpin = value;
                backButton.interactable = !value;
                infoButton.interactable = !value;
                //betPanel.interactable = !value;
                spinButton.interactable = !value;
            }
        }


        public void Open(int _bet)
        {
            slot.Bet = _bet;
            betText.text = _bet + "";
            SetWinText();

            base.Open();
        }
        public override void Open()
        {
            throw new System.Exception();
        }

        private void SetWinText(int _win = 0)
        {
            winText.text = _win > 0 ? Purse.GetMoney(_win) : "";
        }

        private void BackButtonClick()
        {
            //Menu.Open();
            Close();
        }

        private void InfoButtonClick()
        {
            info.Open();
        }

        private void SpinButtonClick()
        {
            if (slot.SpinCounter != 0) { return; }
            if (Purse.Coins.TryBuy(slot.Bet))
            {
                Sound.PlaySpinSound();
                isSpin = true;
                slot.Spin();
            }
        }

        protected override void Awake()
        {
            base.Awake();

            slot = GetComponent<Slot>();

            backButton.onClick.AddListener(BackButtonClick);
            infoButton.onClick.AddListener(InfoButtonClick);

            spinButton.onClick.AddListener(SpinButtonClick);

            slot.OnWin += (_win) => Purse.Coins.Add(_win);
            slot.OnWin += (_win) => isSpin = false;
            slot.OnWin += SetWinText;
            slot.OnWin += (_win) => { if (_win > 0) Sound.PlaySlotWin(); };

            //slot.OnGetBonus += (_gain) => { RiskGame.Open(_gain); };
        }
        
        private void Start()
        {
            //Close();
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveAllListeners();
            infoButton.onClick.RemoveAllListeners();

            spinButton.onClick.RemoveAllListeners();

            slot.OnWin = (_) => { };
        }
    }
}