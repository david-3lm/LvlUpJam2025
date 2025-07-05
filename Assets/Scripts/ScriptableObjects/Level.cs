using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LvlSO", order = 1)]
public class Level : ScriptableObject
{
    public int level;
    public int cols;
    public int rows;
    public List<Vector2> spots;
    public Vector2 startPos;
    
    public GameObject tilePrefab;
    public GameObject spotPrefab;
}
