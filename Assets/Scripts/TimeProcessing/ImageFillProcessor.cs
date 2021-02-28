using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillProcessor : Timer, ITimedItem
{
    private Image image;
    private bool clockwise;

    public ImageFillProcessor(ITimersContainer manager, Image image, float animationTime, bool clockwise, Action callback = null) : base(manager, animationTime, callback)
    {
        this.clockwise = clockwise;
        this.image = image;
    }

    float direction;
    protected override void DoActionWhileTimePassing()
    {
        direction = clockwise ? normalizedTime : (1f - normalizedTime);
        if (image != null)
        {

            image.color = (1f - normalizedTime) > 0.4f ? Color.green : Color.red;

            image.fillAmount = direction;
        }
    }
}