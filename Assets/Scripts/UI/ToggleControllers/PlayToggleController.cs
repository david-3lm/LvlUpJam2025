using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayToggleController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image targetImage;
    [SerializeField] private Image backgroundColorImage;

    [Header("Game References")]
    [SerializeField] private Player player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private FurnitureButtonHandler furnitureButtonHandler;

    [Header("Play Icons")]
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Color playColor = Color.green;

    [Header("Pause Icons")]
    [SerializeField] private Sprite restartSprite;
    [SerializeField] private Color restartColor = Color.red;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    private void Update()
    {
        UpdateIcon(toggle.isOn);
    }

    private void OnToggleChanged(bool isOn)
    {
        UpdateSpeed(isOn);
        UpdateIcon(isOn);
    }

    private void UpdateSpeed(bool isOn)
    {
        if (isOn)
            player.Run();
        else
        {
            player.Stop();
            levelManager.ReloadLevel();
            furnitureButtonHandler.GetLevel();
        }
    }
    public void UpdateIcon(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.sprite = isOn ? restartSprite : playSprite;
            backgroundColorImage.color = isOn ? restartColor : playColor;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
