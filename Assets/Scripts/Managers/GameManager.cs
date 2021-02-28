using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, ITimedItem
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
    [SerializeField] private Transform collectablesParent;

    private List<GameObject> collectablesList;


    [SerializeField] private int maxTimeInSeconds= 5;
    
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


    private Timer timer;


    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        levelController.CreateLevel();
        ui.ShowTapToPlayPanel();
    }

    public void StartPlay() 
    { 
        PlacePlayerInTheLeftBottomCell();
        playerMovement.SetStartAction(true);
        PlaceCollectablesInEveryCell();
        StartTimer();
    }

    private void StartTimer()
    {
        maxTimeInSeconds = levelController.GetNoOfTiles() * 3;
        timer = new Timer(TimeManager.Instance, maxTimeInSeconds, this);
    }

    private void PlacePlayerInTheLeftBottomCell()
    {
        playerMovement.transform.position = levelController.GetLeftBottomCellPosition();
    }

    private void PlaceCollectablesInEveryCell() 
    {
        collectablesList = new List<GameObject>();
        StartCoroutine(levelController.PlaceInEveryCellCoroutine(ResourcesManager.Instance.Heart, collectablesList, collectablesParent));
    
    }

    private void ClearAllCollectables() 
    {   
        if (collectablesParent == null)
            return;

        foreach(Transform child in collectablesParent)
        {
            if(child!=collectablesParent)
                Destroy(child.gameObject);
        }
    }

    internal void ProcessCollecting(Collectable collectable, Action callback = null)
    {
        collectable.enabled = false;
        TimeManager.Instance.Move(collectable.transform, ui.collection.icon.position, 0.5f, null,
            delegate () {
                TotalCollected++;
                if (TotalCollected >= collectablesList.Count) 
                {
                    ui.ShowWinPanel(TotalCollected);
                }
                collectable.gameObject.SetActive(false);
                callback?.Invoke();
            });
    }

    private void Reset() 
    {
        TotalCollected = 0;
        ClearAllCollectables();
    }

    public void ProcessTimePassing()
    {
        ui.timerFillImage.fillAmount = timer.normalizedTime;
        ui.timerText.text = $"{maxTimeInSeconds - (int)timer.elapsedTime}";
    }

    public void ProcessTimeEnded()
    {
        TimeManager.Instance.Remove(timer);
        playerMovement.SetStartAction(false);
        TimeManager.Instance.DelayedCall(1f, ui.TryShowFailedPanel);
    }


    internal void RetryLevel()
    {
        Reset();
        levelController.CreateLevel();
        ui.ShowTapToPlayPanel();
        
    }

    internal void PlayNextLevel()
    {
        Reset();
        levelController.CreateNextLevel();
        ui.ShowTapToPlayPanel();
    }


    void OnDisable() 
    {
        Reset();
    }
}
