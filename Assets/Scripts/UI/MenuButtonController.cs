using System;
using UnityEngine;
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

    [SerializeField] private GameObject _leftBtn;
    [SerializeField] private GameObject _rightBtn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleMenu);

        if (menuContent != null) menuContent.SetActive(isOpen);
        if (backdropMenu != null) backdropMenu.SetActive(isOpen);
    }

	private void Update()
	{
		ClickCouner();
	}

	public void ToggleMenu()
    {
        isOpen = !isOpen;
        if (menuLevels != null) menuLevels.SetActive(false);
        if (menuContent != null) menuContent.SetActive(isOpen);
        if (backdropMenu != null) backdropMenu.SetActive(isOpen);
        if (player != null) player.enabled = !isOpen;
        Debug.Log(isOpen ? "Menu opened" : "Menu closed");
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

	private void ClickCouner()
	{
		throw new NotImplementedException();
	}

	private void OnDestroy()
    {
        if (btn != null)
        {
            btn.onClick.RemoveListener(ToggleMenu);
        }
    }
}
