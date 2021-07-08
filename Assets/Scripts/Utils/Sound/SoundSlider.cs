using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private Slider soundSlider = default;

    private void Awake()
    {
        soundSlider = GetComponent<Slider>();

        soundSlider.onValueChanged.AddListener(SetSound);
        Sound.OnChangeVolume += SetValue;
    }

    private void OnDestroy()
    {
        soundSlider.onValueChanged.RemoveAllListeners();
        Sound.OnChangeVolume -= SetValue;
    }

    private void OnEnable()
    {
        SetValue(Sound.Volume);
    }

    private void SetValue(float _value)
    {
        soundSlider.value = _value;
    }

    private void SetSound(float _volume)
    {
        Sound.Volume = _volume;
    }
}
