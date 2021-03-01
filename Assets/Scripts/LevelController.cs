using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelController : MonoBehaviour
{

    //representation of each level in 0s and 1s
    private List<int[,]> levels = new List<int[,]> {
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

    private int currentLevel = 0;

    private List<Vector3Int> localTilesPositions;
    public TileBase tileBase;
    public GameObject tileMasksParent;
    private Tilemap tileMap;
    private Vector3Int leftBottomCell;
    private SpriteMask[] spriteMasks;

    private void Awake()
    {
        tileMap = GetComponent<Tilemap>();
        spriteMasks = GetComponentsInChildren<SpriteMask>(true);
    }

    public void CreateLevel()
    {
        ClearTiles();
        SetupTiles(levels[currentLevel]);
    }

    internal Vector3 GetLeftBottomCellPosition()
    {
        return tileMap.GetCellCenterWorld(leftBottomCell);
    }

    public void SetupTiles(int[,] levelTiles)
    {

        localTilesPositions = new List<Vector3Int>(levelTiles.GetLength(0) * levelTiles.GetLength(1));
        int yLength = levelTiles.GetLength(0);
        int xLength = levelTiles.GetLength(1);

        int startPosX = Mathf.CeilToInt(-xLength * 0.5f);
        int endPosY = Mathf.CeilToInt((yLength * 0.5f) - 1);

        ///Center TileMap's content
        Vector3 centerScreen = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        tileMap.transform.position = new Vector3(centerScreen.x, centerScreen.y, 0);
        tileMap.transform.position += new Vector3((xLength % 2 == 0 ? 0 : -1), 0, 0);

        for (int x = 0; x < levelTiles.GetLength(1); x++)
        {
            for (int y = 0; y < levelTiles.GetLength(0); y++)
            {
                if (levelTiles[y, x] > 0)
                {
                    Vector3Int localPlace = new Vector3Int(startPosX + x, endPosY - y, 0);
                    localTilesPositions.Add(localPlace);
                }
            }
        }

        //left bottom corner is always existent
        leftBottomCell = new Vector3Int(startPosX, -endPosY - 1, 0);
        AddLeftBottomCell();

        SetupPath(localTilesPositions, tileMap);
    }

    internal int GetNoOfTiles()
    {
        return localTilesPositions.Count;
    }

    private WaitForSeconds waitForSeconds;
    public IEnumerator PlaceInEveryCellCoroutine(GameObject obj, List<GameObject> collectablesList, Transform parent) 
    {
        for (int i = 0; i< localTilesPositions.Count ; i++)
        {
            Vector3 cellPosition = tileMap.CellToWorld(localTilesPositions[i]) + Vector3.one;
            GameObject newCollectable = Instantiate(obj, new Vector3(cellPosition.x, cellPosition.y, 0), Quaternion.identity, parent);
            collectablesList.Add(newCollectable);
            float nextPeriodToWait = UnityEngine.Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(nextPeriodToWait);
        }
    }

    private void AddLeftBottomCell()
    {
        if (localTilesPositions.Contains(leftBottomCell))
        {
            localTilesPositions.Remove(leftBottomCell);
        }
        localTilesPositions.Add(leftBottomCell);
    }

    private void ClearTiles()
    {
        tileMap.ClearAllTiles();

        foreach (SpriteMask mask in spriteMasks) {
            mask.gameObject.SetActive(false);
        }
    }


    internal void CreateNextLevel()
    {
        currentLevel = (currentLevel + 1) % levels.Count;
        CreateLevel();
    }

    private void SetupPath(List<Vector3Int> localTilesPositions, Tilemap baseLevel)
    {
        for (int i=0; i< localTilesPositions.Count; i++)
        {
            baseLevel.SetTile(localTilesPositions[i], tileBase);
            spriteMasks[i].transform.position = tileMap.CellToWorld(localTilesPositions[i]) + Vector3.one;
            spriteMasks[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


}
