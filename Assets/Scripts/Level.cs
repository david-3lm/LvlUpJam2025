using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelNPos
{
    public GameObject model;
    public Vector3 position;
    public Vector3 rotation;
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

    [Header("User Interactuable Furnatures")]

    [Tooltip("This component position works in a range of values: \n"
        + "| x = (-1.5, 2.5) | y = (-4, 4) | z = (-0.5, 0.5) |\n\n"
        + "This component rotation works in statics values: \n"
        + "| x = -20.7 | y = 49 | z = -22.2 |")]

    [Info("This component position works in a range of values: \n"
        + "| x = (-1.5, 2.5) | y = (-4, 4) | z = (-0.5, 0.5) |\n\n"
        + "This component rotation works in statics values: \n"
        + "| x = -20.7 | y = 49 | z = -22.2 |")]
    [SerializeField] public List<ModelNPos> poolFurniture;
    public List<ModelNPos> modelNPoss;
}
