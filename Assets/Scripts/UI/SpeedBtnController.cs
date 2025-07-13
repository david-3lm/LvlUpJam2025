using UnityEngine;
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
    [SerializeField] private Sprite speedDoubleSprite;
    [SerializeField] private Color speedBaseColor = Color.green;
    [SerializeField] private Color speedDoubleColor = Color.red;

    [Header("Caca")]
    [SerializeField] private RectTransform rt;
    private Vector2 originalSize;
    private Vector2 originalPos;


    private void Awake()
    {
        btn.onClick.AddListener(OnButtonClicked);
        originalSize = rt.sizeDelta;
        originalPos = rt.anchoredPosition;
    }

    private void OnButtonClicked()
    {
        player.SwitchSpeed();
        if (targetImage != null)
        {
            targetImage.sprite = player.isDoubleSpeed ? speedDoubleSprite : speedBaseSprite;
            backgroundColorImage.color = player.isDoubleSpeed ? speedDoubleColor : speedBaseColor;

            if (player.isDoubleSpeed)
            {
                rt.sizeDelta = originalSize + new Vector2(15, 15);
                rt.anchoredPosition = originalPos + new Vector2(10, 0);
            }
            else
            {
                rt.sizeDelta = originalSize;
                rt.anchoredPosition = originalPos;
            }
        }
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnButtonClicked);
    }
}
