using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedBtnController : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Player player;

    [Header("UI References")]
    [SerializeField] private Button btn;
    [SerializeField] private Image targetImage;

    [SerializeField] private Image backgroundColorImage;

    [Header("Speed icons")]
    [SerializeField] private Sprite speedBaseSprite;
    [SerializeField] private Color speedBaseColor = Color.green;
    [SerializeField] private Sprite speedDoubleSprite;
    [SerializeField] private Color speedDoubleColor = Color.red;
    [SerializeField] private GameObject doubleSpeedMiniIcon;


    private void Awake()
    {
        btn.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        player.SwitchSpeed();
        if (targetImage != null)
        {
            if (targetImage != null) targetImage.sprite = player.isDoubleSpeed ? speedDoubleSprite : speedBaseSprite;
            if (backgroundColorImage != null) backgroundColorImage.color = player.isDoubleSpeed ? speedDoubleColor : speedBaseColor;
            if (doubleSpeedMiniIcon != null) doubleSpeedMiniIcon.SetActive(player.isDoubleSpeed);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnButtonClicked);
    }
}
