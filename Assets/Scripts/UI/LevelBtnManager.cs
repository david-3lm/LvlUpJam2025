using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBtnsManager : MonoBehaviour
{
    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManagerScript;
    [SerializeField] private MenuButtonController menuButtonController;
    
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
            button.image.color = new Color(0, 1, 0, 1f);
        if (!isUnlocked)
            button.image.color = new Color(1, 0, 0, 1f);
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
