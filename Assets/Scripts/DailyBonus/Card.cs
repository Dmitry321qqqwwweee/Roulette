using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DailyBonus
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private Button cardButton = default;

        [SerializeField]
        private Text winText = default;

        [SerializeField]
        private GameObject winGameObjet = default;

        private Vector2Int moneyWinRange = new Vector2Int(1, 20);

        [SerializeField]
        private Image coinImage = default;

        [SerializeField]
        private DailyBonusUI dailyBonus = default;

        private const float timeToOpenCoinImage = 0.25f;

        private void Awake()
        {
            cardButton.onClick.AddListener(OpenWin);
        }

        private void OpenWin()
        {
            if (dailyBonus.OpenCardInSession) { return; }

            int winValue = Random.Range(moneyWinRange.x, moneyWinRange.y) * 100;

            OpenCard(StateCard.OpenWin);

            string winString = Purse.GetMoney(winValue);

            winText.text = "+" + winString;

            dailyBonus.OpenCardAction.Invoke(winString);

            Purse.Coins.Add(winValue);

            Sound.PlayDailyBonusWin();
        }

        private void CloseWin()
        {
            OpenCard(StateCard.CloseWin);
        }


        private void OpenCard(StateCard stateCard)
        {
            bool state = stateCard == StateCard.OpenWin ? true : false;

            cardButton.gameObject.SetActive(!state);

            winGameObjet.gameObject.SetActive(state);

            if (state) { Invoke(nameof(CoinActive), timeToOpenCoinImage); }
            else { coinImage.gameObject.SetActive(false); }
        }

        private void CoinActive()
        {
            coinImage.gameObject.SetActive(true);
        }

       
    }

    public enum StateCard
    {
        OpenWin,
        CloseWin
    }
}
