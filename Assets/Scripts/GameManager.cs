using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]LevelManager levelManager;
    [SerializeField]GameObject player;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        levelManager.StartGame();
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        if (playerScript.speed != 0)
            CheckPlayerPosition();
    }


    private void CheckPlayerPosition()
    {
        int playerX = (int)(player.transform.position.x + 0.5f);
        int playerY = (int)(player.transform.position.z + 0.5f);
        
        levelManager.CheckStain(playerX, playerY);
    }
}