using UnityEngine;
using UnityEngine.UI;

public class ShowBet : MonoBehaviour
{
    private Text betText = default;

    private void Awake()
    {
        betText = GetComponent<Text>();

        Bet.OnBetChanged += SetBetText;
    }

    private void Start()
    {
        SetBetText(Bet.Get());
    }

    private void OnDestroy()
    {
        Bet.OnBetChanged -= SetBetText;
    }

    private void SetBetText(int _bet) =>
        betText.text = Purse.GetMoney(_bet) + "";
}
