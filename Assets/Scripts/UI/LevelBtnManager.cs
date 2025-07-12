using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBtnsManager : MonoBehaviour
{
    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManagerScript;
    [SerializeField] private MenuButtonController menuButtonController;
    [Header("Sprites")]
    [SerializeField] private Sprite levelUnlockedSprite;
    [SerializeField] private Sprite levelLockedSprite;
    
    public Level levelData;
    public Button button;

    private bool isUnlocked;

    void Start()
    {
        RefreshButton();
    }
    
    public void RefreshButton()
    {
        isUnlocked = LevelProgressManager.IsLevelUnlocked(levelData.id);
        button.interactable = isUnlocked;
        if (isUnlocked)
            button.image.sprite = levelLockedSprite;
        if (!isUnlocked)
            button.image.sprite = levelUnlockedSprite;
    }
    
    public void LoadLevel(int levelID)
    {
        if (LevelProgressManager.IsLevelUnlocked(levelData.id))
        {
            levelManagerScript.LoadLevel(levelID);
            menuButtonController.ClosePauseLevelMenu();
        }
    }
}
