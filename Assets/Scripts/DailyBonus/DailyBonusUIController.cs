using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DailyBonusUIController : MonoBehaviour
{
    [SerializeField]
    private Button activateDailyBonusButton = default;

    private Image activateDailyBonusImage = default;

    [SerializeField]
    private Button dailyBonusButton = default;

    [SerializeField]
    private Button closeDailyBonusButton = default;

    private Image closeDailyBonusImage = default;

    [SerializeField]
    private Text timeText = default;

    [SerializeField]
    private Page dailyBonusPage = default;

    private bool activeDailyBonus = true;

    [SerializeField]
    private DailyBonusImage disableImage = default;

    [SerializeField]
    private DailyBonusImage enableImage = default;


    private void Awake()
    {
        activateDailyBonusImage = activateDailyBonusButton.gameObject.GetComponent<Image>();
        closeDailyBonusImage = closeDailyBonusButton.gameObject.GetComponent<Image>();
        activateDailyBonusButton.onClick.AddListener(AcivateDailyBonus);
        dailyBonusButton.onClick.AddListener(DailyBonusActive);
        closeDailyBonusButton.onClick.AddListener(DisableBonusButton);
    }

    private void AcivateDailyBonus()
    {
        StateDailyBonusButton(true);
    }

    private void DailyBonusActive()
    {
        
        dailyBonusPage.Open();
        StateDailyBonusButton(false);
    }

    private void DisableBonusButton()
    {
        StateDailyBonusButton(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeDailyBonus = !activeDailyBonus;
            SwitchStateDailyImage(activeDailyBonus);
        }    
    }

    public void SwitchStateDailyImage(bool activeDailyBonus)
    {
        dailyBonusButton.interactable = activeDailyBonus;

        if (activeDailyBonus == false)
        {
            timeText.gameObject.SetActive(true);
            activateDailyBonusImage.sprite = disableImage.openButtonSprite;
            closeDailyBonusImage.sprite = disableImage.closeButtonSprite;
            
        }
        else
        {
            timeText.gameObject.SetActive(false);
            activateDailyBonusImage.sprite = enableImage.openButtonSprite;
            closeDailyBonusImage.sprite = enableImage.closeButtonSprite;
        }
    }


    private void StateDailyBonusButton(bool state)
    {
        activateDailyBonusButton.gameObject.SetActive(!state);
        dailyBonusButton.gameObject.SetActive(state);
        closeDailyBonusButton.gameObject.SetActive(state);
    }
}

[System.Serializable]
public class DailyBonusImage
{
    public Sprite openButtonSprite = default;
    public Sprite dailyBonusButtonSprite = default;
    public Sprite closeButtonSprite = default;
}
