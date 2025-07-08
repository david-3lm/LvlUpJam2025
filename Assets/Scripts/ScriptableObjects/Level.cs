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

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LvlSO", order = 1)]
public class Level : ScriptableObject
{
    public int level;
    public int cols;
    public int rows;
    public List<Vector2> spots;
    public List<Vector2> empty;
    public Vector2 startPos;

    public GameObject tilePrefab;
    public GameObject spotPrefab;
    
    public List<Furniture> poolFurniture;
    public List<ModelNPos> modelNPoss;
}
