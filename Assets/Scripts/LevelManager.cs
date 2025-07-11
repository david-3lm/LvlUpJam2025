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
    [SerializeField] private List<Level> levels = new List<Level>();
    public Level currentLevel;

    private int CountCleaned;

    [Header("Levels prefabs")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject smallWall;

    [Header("Scripts")]
    [SerializeField] private Player playerScript;

    private List<GameObject> furnitureBox;

    private const string PREF_LAST_LEVEL = "LastLevelIndex";

    private void Start()
    {
        if (levelRoot == null)
            Debug.LogError("Level root is not assigned in LevelManager!");
        if (furnitureRoot == null)
            Debug.LogError("Furnature root is not assigned in LevelManager!");
        furnitureBox = GameObject.FindGameObjectsWithTag("FurnitureBox").ToList();
        CountCleaned = 0;
        playerScript = player.GetComponent<Player>();
    }

    public void StartGame()
    {
        int last = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 0);
        LoadLevel(last);
    }

    public void LoadLevel(int id)
    {
        RemoveLevel();

        id = Mathf.Clamp(id, 0, levels.Count);
        currentLevel = levels[id];
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
                    if (currentLevel.spots.Contains(new Vector2(i, j)))
                        Instantiate(currentLevel.spotPrefab, new Vector3(i, 1, j), Quaternion.identity, levelRoot).name = $"Stain_{i}_{j}";
                }
            }
            Instantiate(smallWall, new Vector3(i, 0, -1), Quaternion.identity, levelRoot);

            Instantiate(wall, new Vector3(i, 0, currentLevel.cols), Quaternion.identity, levelRoot);
        }
        player.transform.position = new Vector3(currentLevel.startPos.x, 1, currentLevel.startPos.y);
        player.transform.rotation = Quaternion.Euler(0, (float)currentLevel.direction, 0f);

        LoadFurniture(currentLevel.poolFurniture, false, interactuableFurnitureRoot, false);
        LoadFurniture(currentLevel.modelNPoss, true, furnitureRoot, true);
    }

    private void LoadFurniture(List<ModelNPos> list, bool isDecoration, Transform root, bool flagLocalPosition)
    {
        foreach (var item in list)
        {
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
            Debug.Log($"Stain found at ({x}, {y})");
            Destroy(stainTf.gameObject);
            CountCleaned++;
        }
        else
        {
            Debug.Log($"No stain found at ({x}, {y})");
        }
        if (CountCleaned == currentLevel.spots.Count)
        {
            NextLevel();
            CountCleaned = 0;
            playerScript.Stop();
        }
    }

    public void NextLevel()
    {
        //int nextIndex = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 0) + 1;
        int nextIndex = currentLevel.id + 1;
        Debug.Log("current level id +1 ---> " + (currentLevel.id + 1));
        if (nextIndex < levels.Count)
        {
            LoadLevel(nextIndex);
        }
        else
        {
            Debug.Log("No more levels available.");
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
        LoadLevel(currentLevel.id);
    }
}
