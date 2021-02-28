using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour, ITimersContainer
{

    private static TimeManager instance;
    public static TimeManager Instance
    {
        get
        {
            if (instance == null && !Application.isPlaying)
                instance = FindObjectOfType<TimeManager>();

            return instance;
        }
    }

    [SerializeField] private Movement movements;

    public List<ITimedItem> containedItems { get; set; }

    private void Awake()
    {
        instance = this;
        containedItems = new List<ITimedItem>();
    }


    private void Update()
    {
        for (int i = 0; i < containedItems.Count; i++)
        {
            containedItems[i]?.ProcessTimePassing();
        }
    }

    public Timer DelayedCall(float delayTime, Action callback)
    {
        Timer delayedCall = new Timer(this, delayTime,null, callback);
        containedItems.Add(delayedCall);
        return delayedCall;
    }

    public ImageFillProcessor FillImageProgress(Image image, float animationTime, bool clockwise, Action callback, ITimedItem timedItem = null)
    {
        ImageFillProcessor imageFillProcessor = new ImageFillProcessor(this, image, animationTime, clockwise, timedItem,  callback);
        containedItems.Add(imageFillProcessor);
        return imageFillProcessor;
    }

    public void FillImageProgress(ImageFillProcessor imageFillProcessor)
    {
        containedItems.Add(imageFillProcessor);
    }

    public MovingObject Move(Transform objToMove, Vector3 moveTo, float animationTime, ITimedItem timedItem = null, Action callback = null)
    {
        MovingObject movingObject = new MovingObject(this, movements[0], objToMove, moveTo, animationTime, timedItem,  callback);
        containedItems.Add(movingObject);
        return movingObject;
    }

    public void Remove(ITimedItem item)
    {
        containedItems.Remove(item);
        item = null;
    }
}
