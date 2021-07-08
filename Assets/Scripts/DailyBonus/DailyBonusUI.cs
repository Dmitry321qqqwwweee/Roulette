using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DailyBonus
{
    public class DailyBonusUI : Page
    {
        [SerializeField]
        private Button closeButton = default;

        [SerializeField]
        private DailyBonusWinUI dailyBonusWinUI = default;

        public bool OpenCardInSession { get; private set; } = false;

        public Action<String> OpenCardAction { get; private set; } = default;

        public override void Open()
        {
            base.Open();
            Sound.PlayDailyBonus();
        }

        protected override void Awake()
        {
            base.Awake();

            closeButton.onClick.AddListener(CloseButtonClick);

            OpenCardAction += OpenCard;
        }

        private void CloseButtonClick()
        {
            dailyBonusWinUI.Close();

            Close();
        }

        private void OpenCard(string winText)
        {
            OpenCardInSession = true;

            StartCoroutine(OpenWinPage(winText));
        }

        private IEnumerator OpenWinPage(string winText)
        {
            yield return new WaitForSeconds(1f);

            dailyBonusWinUI.OpenWithWinValue(winText);

            yield return new WaitForSeconds(3f);

            CloseButtonClick();


        }

    }
}