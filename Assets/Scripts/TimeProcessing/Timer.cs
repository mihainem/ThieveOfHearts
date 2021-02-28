using System;
using UnityEngine;

public class Timer : ITimedItem, IDisposable
{
    protected float elapsedTime = 0f;
    protected float animationTime = 1f;
    protected float normalizedTime;
    protected Action callback;
    protected ITimersContainer container;

    public Timer(ITimersContainer container, float animationTime, Action callback = null)
    {
        this.container = container;
        this.callback = callback;
        this.animationTime = animationTime;
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

            ProcessTimeEnded();
        }
        normalizedTime = elapsedTime / animationTime;

        DoActionWhileTimePassing();
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
    }
}