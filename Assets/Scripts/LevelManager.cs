using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Roots")]
    [Tooltip("Content for tiles and spots")]
    [SerializeField] private Transform levelRoot;

    [Tooltip("Content for furniture models")]
    [SerializeField] public Transform furnitureRoot;

    [Tooltip("Content for user interactuable furnitures")]
    [SerializeField] private Transform interactuableFurnitureRoot;

    [Header("Levels list")]
    [SerializeField] private List<Level> levelList = new List<Level>();
    [SerializeField] private Dictionary<int, Level> levels = new Dictionary<int, Level>();
    public Level currentLevel;

    private int CountCleaned;
    [Header("Lista de botones de niveles")]
    [SerializeField] private LevelBtnsManager[] levelButtons;

    [Header("Levels prefabs")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject smallWall;

    [Header("Scripts")]
    [SerializeField] private Player playerScript;
    
    [SerializeField] Animator UIAnimator;

    [SerializeField] private GameObject tutorialCanvas;
    
    [SerializeField] int DebugLvl = 0;

    private const string PREF_LAST_LEVEL = "LastLevelIndex";

    private void Awake()
    {
        foreach (var l in levelList)
        {
            levels.TryAdd(l.id, l);
        }
        LevelProgressManager.UnlockLevel(1);
    }
    
    private void Start()
    {
        if (levelRoot == null)
            Debug.LogError("Level root is not assigned in LevelManager!");
        if (furnitureRoot == null)
            Debug.LogError("Furnature root is not assigned in LevelManager!");
        CountCleaned = 0;
        playerScript = player.GetComponent<Player>();
    }
    
    public void StartGame()
    {
        int last = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 1);
        
        #if UNITY_EDITOR
            last = DebugLvl;
        #endif
        
        LoadLevel(last);
    }

    public void LoadLevel(int id)
    {
        RemoveLevel();
        if (!levels.ContainsKey(id))
        {
            Debug.LogError($"Level with ID {id} not found!");
            return;
        }
        currentLevel = levels[id];
        if (currentLevel.id == 1)
            tutorialCanvas.SetActive(true);
        else
            tutorialCanvas.SetActive(false);
        PlayerPrefs.SetInt(PREF_LAST_LEVEL, id);
        PlayerPrefs.Save();
        Debug.Log($"Loading level {id}: {currentLevel.name}");
        
        for (int i = 0; i < currentLevel.rows; i++)
        {
            for (int j = 0; j < currentLevel.cols; j++)
            {
                
                if (currentLevel.empty.Contains(new Vector2(i, j)))
                {
                    Instantiate(smallWall, new Vector3(i, 0, j), Quaternion.identity, levelRoot);
                } else
                {
                    Instantiate(wall, new Vector3(-1, 0, j), Quaternion.identity, levelRoot);
                    Instantiate(currentLevel.tilePrefab, new Vector3(i, 0, j), Quaternion.identity, levelRoot);
                    Instantiate(smallWall, new Vector3(currentLevel.rows, 0, j), Quaternion.identity, levelRoot);
                    var idx = Random.Range(0, currentLevel.spotPrefab.Count);
                    if (currentLevel.spots.Contains(new Vector2(i, j)))
                        Instantiate(currentLevel.spotPrefab[idx], new Vector3(i, 1, j), Quaternion.identity, levelRoot).name = $"Stain_{i}_{j}";
                }
            }
            Instantiate(smallWall, new Vector3(i, 0, -1), Quaternion.identity, levelRoot);

            Instantiate(wall, new Vector3(i, 0, currentLevel.cols), Quaternion.identity, levelRoot);
        }


        //LoadFurniture(currentLevel.poolFurniture, false, interactuableFurnitureRoot, false);
        LoadFurniture(currentLevel.modelNPoss, true, furnitureRoot, true);
        RestartPositionRotation();
    }

    private void LoadFurniture(List<ModelNPos> list, bool isDecoration, Transform root, bool flagLocalPosition)
    {
        foreach (var item in list)
        {
            if (item.model == null)
            {
                Debug.LogWarning($"Model is null for item at position {item.position} with rotation {item.rotation}");
                continue;
            }
            GameObject go = Instantiate(item.model, item.position, Quaternion.Euler(item.rotation));

            var furniture = go.GetComponent<Furniture>();
            if (furniture != null) furniture.isDecoration = isDecoration;

            go.transform.SetParent(root, flagLocalPosition);
        }
    }

    public void CheckStain(int x, int y)
    {
        Transform stainTf = levelRoot.Find($"Stain_{x}_{y}");
        if (stainTf != null)
        {
            //Debug.Log($"Stain found at ({x}, {y})");
            playerScript.Collect();
            Destroy(stainTf.gameObject);
            CountCleaned++;
        }
        else
        {
            //Debug.Log($"No stain found at ({x}, {y})");
        }
        if (CountCleaned == currentLevel.spots.Count)
        {
            CountCleaned = 0;
            StartCoroutine(EndLevelCoroutine());
        }
    }

    IEnumerator EndLevelCoroutine()
    {
        UIAnimator.SetBool("Win",true);
        playerScript.Stop();
        yield return new WaitForSeconds(1f);
        NextLevel();
        yield return new WaitForSeconds(1f);
        UIAnimator.SetBool("Win",false);
    }
    
    public void RefreshAllLevelButtons()
    {
        LevelBtnsManager[] buttons = FindObjectsOfType<LevelBtnsManager>();

        foreach (var btn in levelButtons)
        {
            btn.RefreshButton();
        }
    }
    
    public void OnLevelCompleted()
    {
        int nextLevelIndex = currentLevel.id + 1;
        LevelProgressManager.UnlockLevel(nextLevelIndex);
        RefreshAllLevelButtons();
    }
    
    public void NextLevel()
    {
        //int nextIndex = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 0) + 1;
        int nextIndex = currentLevel.id + 1;
        //Debug.Log("current level id +1 ---> " + (currentLevel.id + 1));
        if (nextIndex < levels.Count)
        {
            playerScript.Stop();
            OnLevelCompleted();
            LoadLevel(nextIndex);
        }
        else
        {
            //Debug.Log("No more levels available.");
        }
    }

    public void RemoveLevel()
    {
        void ClearTransform(Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
                Destroy(parent.GetChild(i).gameObject);
        }

        ClearTransform(levelRoot);
        ClearTransform(furnitureRoot);
        ClearTransform(interactuableFurnitureRoot);
    }

    public void ReloadLevel()
    {
        playerScript.gameStarted = false;
        CountCleaned = 0;
        LoadLevel(currentLevel.id);
    }
    
    private void RestartPositionRotation()
    {
        player.transform.position = new Vector3(currentLevel.startPos.x, 1.5f, currentLevel.startPos.y);
        player.transform.rotation = Quaternion.Euler(0, (float)currentLevel.direction, 0f);
    }
}
