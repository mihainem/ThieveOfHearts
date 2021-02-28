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
            ui.timerText.text = _timer.ToString();
        }
    }

    private void Awake()
    {
        instance = this;
        
    }

    private void OnTimerEnded()
    {
        Debug.Log("Timer Ended");
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
        StartTimer();
    }

    private void StartTimer()
    {
        timer = new Timer(TimeManager.Instance, 30f, this);
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
        TimeManager.Instance.Move(collectable.transform, ui.collection.icon.position, 2f, null,
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

    public void ProcessTimePassing()
    {
        ui.timerFillImage.fillAmount = timer.normalizedTime;
        Timer = 30 - (int)timer.elapsedTime;
    }

    public void ProcessTimeEnded()
    {
        Debug.Log(timer.elapsedTime);
        //throw new NotImplementedException();
    }
}
