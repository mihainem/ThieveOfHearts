using System;
using UnityEngine;

public class MovingObject : Timer, ITimedItem
{
    private Vector3 positionOffset;
    private Transform objToMove;
    private Vector3 moveFrom;
    private Vector3 moveTo;
    private bool shouldAnimateX;
    private MovementCurve movement;

    public MovingObject(ITimersContainer manager, MovementCurve movementCurve, Transform objToMove, Vector3 moveTo, float animationTime,  Action callback = null) : base(manager, animationTime, null, callback)
    {
        this.movement = movementCurve;
        this.objToMove = objToMove;
        moveFrom = objToMove.position;
        this.moveTo = moveTo;
        shouldAnimateX = true; // Mathf.Abs(moveTo.y - moveFrom.y) > Mathf.Abs(moveTo.x - moveFrom.x);
    }

    protected override void DoActionWhileTimePassing()
    {
        positionOffset = (shouldAnimateX ? Vector3.right : Vector3.up) * movement.curve.Evaluate(normalizedTime) * movement.curveStrength ;

        if (objToMove != null)
        {
            objToMove.position = Vector3.Lerp(moveFrom, moveTo - positionOffset, normalizedTime) + positionOffset;
        }
        else
        {
            container.Remove(this);
        }
    }
}
