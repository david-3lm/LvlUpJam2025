using UnityEngine;
using UnityEngine.UI;

public class MuteToggleController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite mutedSprite;
    [SerializeField] private Sprite unmutedSprite;

    private const string PREF_MUTE = "Mute";

    private void Awake()
    {
        bool isMuted = PlayerPrefs.GetInt(PREF_MUTE, 0) == 1;
        toggle.SetIsOnWithoutNotify(!isMuted);
        UpdateIcon(!isMuted);
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt(PREF_MUTE, isOn ? 0 : 1);
        UpdateIcon(isOn);
    }

    private void UpdateIcon(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.sprite = isOn ? mutedSprite : unmutedSprite;
        }
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
