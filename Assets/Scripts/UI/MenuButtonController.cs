using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField] private GameObject menuContent;
    [SerializeField] private Player player;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OpenMenu);
    }

    public void OpenMenu()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(true);
            Debug.Log("Menu opened");
        }
        if (player != null)
            player.enabled = false;
    }

    public void CloseMenu()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(false);
            Debug.Log("Menu closed");
        }

        if (player  != null)
            player.enabled = true;
    }

    private void OnDestroy()
    {
        if (btn != null)
        {
            btn.onClick.RemoveListener(OpenMenu);
        }
    }
}
