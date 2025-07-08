using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    private Level currentLevel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject smallWall;
    private List<GameObject> tiles = new List<GameObject>();
    private List<GameObject> stains = new List<GameObject>();
    public void StartGame()
    {
        currentLevel = levels[0];
        LoadLevel();
    }
    
    public void LoadLevel()
    {
        GameObject go;
        for (int i = 0; i < currentLevel.rows; i++)
        {
            for (int j = 0; j < currentLevel.cols; j++)
            {
                if (currentLevel.empty.Contains(new Vector2(i,j)))
                {
                    Instantiate(smallWall, new Vector3(i, 0, j), Quaternion.identity);
                    continue;
                }
                Instantiate(wall, new Vector3(-1, 0, j), Quaternion.identity);
                go = Instantiate(currentLevel.tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                tiles.Add(go);
                if (currentLevel.spots.Contains(new Vector2(i, j)))
                {
                    go = Instantiate(currentLevel.spotPrefab, new Vector3(i, 1, j), Quaternion.identity);
                    stains.Add(go);
                }
            }
            Instantiate(wall, new Vector3(i, 0, currentLevel.cols), Quaternion.identity);
        }
        player.transform.position = new Vector3(currentLevel.startPos.x, 1, currentLevel.startPos.y);
        LoadFurniture();
    }

    private void LoadFurniture()
    {
        foreach (var furniture in currentLevel.modelNPoss)
        {
            var go = Instantiate(furniture.model, new Vector3(furniture.position.x, 0, furniture.position.y), Quaternion.identity);
            go.transform.RotateAround(go.transform.position, Vector3.up, furniture.rotation);
        }
    }

    public void CheckStain(int x, int y)
    {
        foreach (GameObject go in stains)
        {
            if ((int)go.transform.position.x == x && (int)go.transform.position.z == y)
            {
                go.SetActive(false);
            }
        }
    }
}
