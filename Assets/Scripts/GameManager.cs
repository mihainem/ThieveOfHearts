using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<int[,]> levelTiles = new List<int[,]> {
        new int[,]{
        {1,1,1,0,1,0,1},
        {0,0,1,1,1,1,1},
        {0,0,1,0,1,0,1},
        {1,1,1,0,1,0,1} 
        },
        new int[,]{
        {1,1,1,0,1,1,1},
        {1,0,1,1,1,0,1},
        {1,1,1,0,1,1,1},
        {1,0,1,1,1,0,1}}
    };

    public LevelController levelController;
    public UIController ui;
    public PlayerMovement playerMovement;


    private void Start()
    {
        CreateLevel(0);
        PlacePlayerInTheLeftBottomCell();
    }

    private void CreateLevel(int levelIndex)
    {
        levelController.SetupTiles(levelTiles[levelIndex]);
    }

    private void PlacePlayerInTheLeftBottomCell()
    {
        levelController.PlaceInLeftBottomCell(playerMovement);
    }

 
}
