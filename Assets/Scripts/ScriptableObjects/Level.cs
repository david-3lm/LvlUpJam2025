using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelNPos
{
    public GameObject model;
    public Vector2 position;
    public float rotation;
}

public enum Direction
{
    North = 0,
    East = 90,
    South = 180,
    West = 270
}

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LvlSO", order = 1)]
public class Level : ScriptableObject
{
    public int level;
    public int cols;
    public int rows;
    public List<Vector2> spots;
    public List<Vector2> empty;
    public Vector2 startPos;
    public Direction direction;

    public GameObject tilePrefab;
    public GameObject spotPrefab;
    
    public List<GameObject> poolFurniture;
    public List<ModelNPos> modelNPoss;
}
