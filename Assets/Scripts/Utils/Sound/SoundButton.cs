using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private Button soundOnButton = default;
    [SerializeField]
    private Button soundOffButton = default;

    private void SoundButtonClick()
    {
        Sound.IsMute = !Sound.IsMute;
    }

    private void SetSound(bool isMute)
    {
        soundOnButton.gameObject.SetActive(isMute);
        soundOffButton.gameObject.SetActive(!isMute);
    }

    private void Awake()
    {
        soundOnButton.onClick.AddListener(SoundButtonClick);
        soundOffButton.onClick.AddListener(SoundButtonClick);

        Sound.OnChangeIsMute += SetSound;
    }

    private void Start()
    {
        SetSound(Sound.IsMute);
    }

    private void OnDestroy()
    {
        soundOnButton.onClick.RemoveAllListeners();
        soundOffButton.onClick.RemoveAllListeners();

        Sound.OnChangeIsMute += SetSound;
    }
}
