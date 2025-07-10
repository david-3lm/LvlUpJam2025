using UnityEngine;
using UnityEngine.UI;

public class PlayToggleController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image targetImage;

    [Header("Play Icons")]
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Color playColor = Color.green;

    [Header("Pause Icons")]
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Color pauseColor = Color.red;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        UpdateIcon(isOn);
    }

    private void UpdateIcon(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.sprite = isOn ? pauseSprite : playSprite;
            targetImage.color = isOn ? pauseColor : playColor;
        }
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
