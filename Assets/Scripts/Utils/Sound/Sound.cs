using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Action<float> OnChangeVolume = (float _volume) => { };
    private const string VOLUME = "Volume";
    public static float Volume
    {
        get
        {
            return PlayerPrefs.GetFloat(VOLUME, 1);
        }
        set
        {
            if (value < 0) { value = 0; }
            if (value > 1) { value = 1; }
            PlayerPrefs.SetFloat(VOLUME, value);
            OnChangeVolume(value);
        }
    }

    public static Action<bool> OnChangeIsMute = (bool _isMute) => { };
    private const string IS_MUTE = "IsMute";
    public static bool IsMute
    {
        get
        {
            return PlayerPrefs.GetInt(IS_MUTE, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(IS_MUTE, value ? 1 : 0);
            OnChangeIsMute(value);
        }
    }

    private AudioSource backgroundAudioSource = default;
    private List<AudioSource> audioSources = new List<AudioSource>();

    private static Sound instance = default;

    private void Awake()
    {
        audioSources.AddRange(GetComponentsInChildren<AudioSource>());
        backgroundAudioSource = audioSources[0];
        audioSources.RemoveAt(0);

        OnChangeIsMute += SetVolume;
        OnChangeVolume += SetVolume;

        instance = this;
    }

    private void OnDestroy()
    {
        OnChangeIsMute -= SetVolume;
        OnChangeVolume -= SetVolume;
    }

    private void SetVolume(float _volume)
    {
        backgroundAudioSource.volume = _volume;
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = _volume;
        }
    }

    private void SetVolume(bool _isMute)
    {
        SetVolume(_isMute ? 0 : Volume);
    }

    private void SetBackGroundSound(AudioClip _audioClip)
    {
        backgroundAudioSource.clip = _audioClip;
        backgroundAudioSource.Play();
    }

    private void PlaySound(AudioClip _audioClip)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = _audioClip;
                audioSource.Play();
                return;
            }
        }
    }

    /////////////////

    public static void SetBG(AudioClip _audioClip) =>
        instance.SetBackGroundSound(_audioClip);

    [SerializeField]
    private AudioClip winRoulette = default;
    public static void PlayWinRoulette() =>
        instance.PlaySound(instance.winRoulette);

    [SerializeField]
    private AudioClip selectChip = default;
    public static void PlaySelectChip() =>
        instance.PlaySound(instance.selectChip);

    [SerializeField]
    private AudioClip spinButton = default;
    public static void PlaySpinButton() =>
        instance.PlaySound(instance.spinButton);

    [SerializeField]
    private AudioClip chipsSound = default;
    public static void PlayChipsSound() =>
        instance.PlaySound(instance.chipsSound);

    [SerializeField]
    private AudioClip betAdd = default;
    public static void PlayBetAdd() =>
        instance.PlaySound(instance.betAdd);

    [SerializeField]
    private AudioClip dailyBonus = default;
    public static void PlayDailyBonus() =>
        instance.PlaySound(instance.dailyBonus);

    [SerializeField]
    private AudioClip increaseBet = default;
    public static void PlayIncreaseBet() =>
        instance.PlaySound(instance.increaseBet);

    [SerializeField]
    private AudioClip dailyBonusWin = default;
    public static void PlayDailyBonusWin() =>
        instance.PlaySound(instance.dailyBonusWin);

    [SerializeField]
    private AudioClip wheelSound = default;
    public static void PlayWheelSound() =>
        instance.PlaySound(instance.wheelSound);

    [SerializeField]
    private AudioClip spinSound = default;
    public static void PlaySpinSound() =>
        instance.PlaySound(instance.spinSound);

    [SerializeField]
    private AudioClip slotWin = default;
    public static void PlaySlotWin() =>
        instance.PlaySound(instance.slotWin);
}
