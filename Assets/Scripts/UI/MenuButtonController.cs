using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField] private GameObject menuContent;
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
    }

    private void OnDestroy()
    {
        if (btn != null)
        {
            btn.onClick.RemoveListener(OpenMenu);
        }
    }
}
