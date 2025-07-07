using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    private const string exposedParam = "MasterVolume";
    private const float minDb = -80f;

    [Header("UI References")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private TMP_Text volumePercentText;

    private const string PREF_VOL = "MasterVolumeSlider";
    private const string PREF_MUTED = "Muted";

    // Start is called before the first frame update
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(PREF_VOL, 0.5f);
        bool savedMuted = PlayerPrefs.GetInt(PREF_MUTED, 0) == 1;

        volumeSlider.SetValueWithoutNotify(savedVolume);
        muteToggle.SetIsOnWithoutNotify(savedMuted);

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
        muteToggle.onValueChanged.AddListener(OnToggleChanged);

        OnToggleChanged(savedMuted);
        UpdatePercentText(savedVolume);
    }

    private void OnSliderChanged(float linear)
    {
        PlayerPrefs.SetFloat(PREF_VOL, linear);

        bool isMuted = muteToggle.isOn;
        float dB = (isMuted || linear <= 0.0001f) ? minDb : Mathf.Log10(linear) * 20f;
        audioMixer.SetFloat(exposedParam, dB);
        UpdatePercentText(linear);
    }

    private void OnToggleChanged(bool isMuted)
    {
        PlayerPrefs.SetInt(PREF_MUTED, isMuted ? 1 : 0);

        if (isMuted)
            audioMixer.SetFloat(exposedParam, minDb);
        else
            OnSliderChanged(volumeSlider.value);
    }

    private void UpdatePercentText(float linear)
    {
        if (volumePercentText != null)
            volumePercentText.text = $"{Mathf.RoundToInt(linear * 100f)}%";
    }

    void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnSliderChanged);
        muteToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}