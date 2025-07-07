using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMuteManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private bool isMuted = false;
    private const string exposedParam = "MasterVolume";
    private const float mutedDb = -80f;
    private const float unmutedDb = 0f;

    void Start()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        ApplyVolume();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        ApplyVolume();
        Debug.Log($"AudioMuteManager: Mute toggled to {isMuted}");
    }

    private void ApplyVolume()
    {
        if (audioMixer == null) return;
        float targetDb = isMuted ? mutedDb : unmutedDb;
        audioMixer.SetFloat(exposedParam, targetDb);
    }
}
