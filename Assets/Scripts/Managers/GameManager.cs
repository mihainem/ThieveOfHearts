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

    [SerializeField] private UIController ui;

    [SerializeField] private LevelController levelController;
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private int timeToWaitForEveryTile = 3;
    private Timer timer;
    private int maxTimeInSeconds = 30;
    
    
    [SerializeField] private Transform collectablesParent;
    private List<GameObject> collectablesList;
    private float flyingCollectableTime = 1f;
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
        playerController.SetStartAction(true);
        PlaceCollectablesInEveryCell();
        StartTimer();
    }

    private void StartTimer()
    {
        maxTimeInSeconds = levelController.GetNoOfTiles() * timeToWaitForEveryTile;
        timer = new Timer(TimeManager.Instance, maxTimeInSeconds, this);
    }

    private void PlacePlayerInTheLeftBottomCell()
    {
        playerController.transform.position = levelController.GetLeftBottomCellPosition();
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
        TimeManager.Instance.Move(collectable.transform, ui.collection.icon.position, flyingCollectableTime, null,
            delegate () {
                TotalCollected++;
                if (TotalCollected >= collectablesList.Count)
                {
                    ProcessTimeEnded();
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
        playerController.SetStartAction(false);
        TimeManager.Instance.DelayedCall(flyingCollectableTime, ShowWinOrFail);
    }

    private void ShowWinOrFail()
    {

        if (TotalCollected >= collectablesList.Count)
        {
            ui.ShowWinPanel(TotalCollected);
        }
        else
        {
            ui.TryShowFailedPanel();
            
        }
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
