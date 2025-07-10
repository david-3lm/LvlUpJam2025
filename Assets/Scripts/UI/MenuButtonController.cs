using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    [Header("Menu Content")]
    [SerializeField] private GameObject menuContent;
    [SerializeField] private GameObject backdropMenu;

    [Header("Player Controller")]
    [SerializeField] private Player player;

    private Button btn;
    
    [Header("Menu State")]
    [SerializeField] private bool isOpen = false;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleMenu);

        if (menuContent != null) menuContent.SetActive(isOpen);
        if (backdropMenu != null) backdropMenu.SetActive(isOpen);
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        Debug.Log("Toggling menu state: " + isOpen);
        if (menuContent != null) menuContent.SetActive(isOpen);
        if (backdropMenu != null)
        { 
            backdropMenu.SetActive(isOpen);
        }
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

    private void OnDestroy()
    {
        if (btn != null)
        {
            btn.onClick.RemoveListener(ToggleMenu);
        }
    }
}
