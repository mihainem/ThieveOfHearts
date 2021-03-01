using System;
using UnityEngine;

public class Timer : ITimedItem, IDisposable
{
    public float elapsedTime = 0f;
    protected float animationTime = 1f;
    public float normalizedTime;
    protected Action callback;
    protected ITimersContainer container;
    protected ITimedItem timedItem;

    public Timer(ITimersContainer container, float animationTime, ITimedItem timedItem = null, Action callback = null)
    {
        this.container = container;
        this.callback = callback;
        this.animationTime = animationTime;
        this.timedItem = timedItem;
        container.containedItems.Add(this);
    }

    public void Dispose()
    {
        container.Remove(this);
    }

    public void ProcessTimePassing()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= animationTime)
        {
            elapsedTime -= animationTime;

            if (timedItem == null)
            {
                ProcessTimeEnded();
            }
            else
            {
                timedItem.ProcessTimeEnded();
            }
        }
        normalizedTime = elapsedTime / animationTime;


        DoActionWhileTimePassing();

        timedItem?.ProcessTimePassing();

    }

    protected virtual void DoActionWhileTimePassing() { }

    public void ExecuteCallback()
    {
        callback?.Invoke();
    }

    public void ProcessTimeEnded()
    {
        callback?.Invoke();
        container.Remove(this);
        callback = null;
    }
}