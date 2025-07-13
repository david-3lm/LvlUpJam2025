using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    [Header("Menu Content")]
    [SerializeField] private GameObject menuContent;
    [SerializeField] private GameObject menuLevels;
    [SerializeField] private GameObject backdropMenu;

    [Header("Player Controller")]
    [SerializeField] private Player player;

    private Button btn;
    
    [Header("Menu State")]
    [SerializeField] private bool isOpen = false;

    [SerializeField] private Button _leftBtn;
    int leftBtnClicksCount;
    [SerializeField] private Toggle _rightBtn;
    int rightBtnClicksCount;
    [SerializeField] private TMP_Text soundNum;
    [SerializeField] private GameObject backgroundMenu;
    [SerializeField] private Button backgroundBtn;
    private bool _isBackroundOn = false;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleMenu);
        _leftBtn.onClick.AddListener(OnLeftBtnClicked);
        _rightBtn.onValueChanged.AddListener(OnRightBtnClicked);

        if (menuContent != null) menuContent.SetActive(isOpen);
        if (backdropMenu != null) backdropMenu.SetActive(isOpen);
    }

	private void Update()
	{
        LaunchMenu();
	}

	public void ToggleMenu()
    {
        isOpen = !isOpen;
        if (menuLevels != null) menuLevels.SetActive(false);
        if (menuContent != null)
        {
            menuContent.SetActive(isOpen);
            if (!isOpen)
                EventSystem.current.SetSelectedGameObject(null);
        }
        if (backdropMenu != null) backdropMenu.SetActive(isOpen);
        if (player != null) player.enabled = !isOpen;
    }

    public void CloseMenu()
    {
        if (!isOpen) return;

        isOpen = false;
        
        if (menuContent != null) menuContent.SetActive(false);
        if (backdropMenu != null) backdropMenu.SetActive(false);
        if (player  != null) player.enabled = true;
    }

    public void OpenLevelMenu()
    {
        if (menuLevels != null)
            menuLevels.SetActive(true);
    }

    public void ClosePauseLevelMenu()
    {
        CloseMenu();
        if (menuLevels != null)
            menuLevels.SetActive(false);
    }

    private void OnLeftBtnClicked()
    {
        leftBtnClicksCount++;
    }

    private void OnRightBtnClicked(bool isOn)
	{
        rightBtnClicksCount++;
    }

	private void OnDestroy()
    {
        if (btn != null)
        {
            btn.onClick.RemoveListener(ToggleMenu);
        }
    }

    public void LaunchMenu()
    {
        if (leftBtnClicksCount == 6 && rightBtnClicksCount == 9 && soundNum.text.Equals("69%", StringComparison.OrdinalIgnoreCase) && !_isBackroundOn)
        {
            backgroundBtn.gameObject.SetActive(true);
        }
	}

    public void ShowMenu()
    {
        Instantiate(backgroundMenu);
        _isBackroundOn = true;
    }
}
