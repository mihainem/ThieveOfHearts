using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null && !Application.isPlaying)
                instance = GameObject.FindObjectOfType<GameManager>();

            return instance;
        }
    }

    [SerializeField] private Movement movements;
    [SerializeField] private LevelController levelController;
    [SerializeField] private UIController ui;
    [SerializeField] private PlayerMovement playerMovement;

    private int _totalCollected;
    public int TotalCollected 
    {
        get 
        {
            return _totalCollected;   
        }
        set {
            _totalCollected = value;
            ui.collection.elementValue.text = _totalCollected.ToString();
        }
    }

    private int _timer;
    public int Timer
    {
        get
        {
            return _timer;
        }
        set
        {
            _timer = value;
            ui.timeElapsing.text = _timer.ToString();
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelController.CreateLevel();
    }

    public void StartPlay() 
    { 
        PlacePlayerInTheLeftBottomCell();
        playerMovement.SetStartAction(true);
        PlaceCollectablesInEveryCell();
    }

  

    private void PlacePlayerInTheLeftBottomCell()
    {
        playerMovement.transform.position = levelController.GetLeftBottomCellPosition();
    }

    private void PlaceCollectablesInEveryCell() 
    {
        GameObject collectables = new GameObject();
        collectables.name = "Collectables";
        collectables.transform.localScale = Vector3.one;
        collectables.AddComponent<RectTransform>();
        collectables.transform.SetParent(ui.worldCanvas.transform,false);
        StartCoroutine(levelController.PlaceInEveryCellCoroutine(ResourcesManager.Instance.Heart, collectables.transform));
    }

    internal void ProcessCollecting(Collectable collectable, Action callback = null)
    {
        collectable.enabled = false;
        TimeManager.Instance.Move(collectable.transform, ui.collection.icon.position, 2f, 
            delegate () {
                TotalCollected++;
                collectable.gameObject.SetActive(false);
                callback?.Invoke();
            });
    }

    private void Reset() 
    {
        TotalCollected = 0;
        Timer = 30;
    }
}
