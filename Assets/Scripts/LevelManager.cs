using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Roots")]
    [Tooltip("Content for tiles and spots")]
    [SerializeField] private Transform levelRoot;

    [Tooltip("Content for furniture models")]
    [SerializeField] private Transform furnitureRoot;

    [Header("Levels list")]
    [SerializeField] private List<Level> levels = new List<Level>();
    private Level currentLevel;

    [Header("Levels prefabs")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject smallWall;

    private const string PREF_LAST_LEVEL = "LastLevelIndex";

    private void Start()
    {
        if (levelRoot == null)
            Debug.LogError("Level root is not assigned in LevelManager!");
        if (furnitureRoot == null)
            Debug.LogError("Furnature root is not assigned in LevelManager!");
    }

    public void StartGame()
    {
        int last = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 0);
        LoadLevel(last);
    }

    public void LoadLevel(int id)
    {
        RemoveLevel();

        id = Mathf.Clamp(id, 0, levels.Count - 1);
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

        LoadFurniture();
    }

    private void LoadFurniture()
    {
        foreach (var furniture in currentLevel.modelNPoss)
            Instantiate(furniture.model, new Vector3(furniture.position.x, 0, furniture.position.y), Quaternion.Euler(0, furniture.rotation, 0), furnitureRoot);
    }

    public void CheckStain(int x, int y)
    {
        Transform stainTf = levelRoot.Find($"Stain_{x}_{y}");
        if (stainTf != null)
        {
            //Debug.Log($"Stain found at ({x}, {y})");
            Destroy(stainTf.gameObject);
        }
        else
        {
            //Debug.Log($"No stain found at ({x}, {y})");
        }
    }

    public void NextLevel()
    {
        int nextIndex = PlayerPrefs.GetInt(PREF_LAST_LEVEL, 0) + 1;
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
    }
}
