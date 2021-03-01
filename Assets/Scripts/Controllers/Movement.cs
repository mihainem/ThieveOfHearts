using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public List<MovementCurve> movementCurves;

    public MovementCurve this[int index]
    {
        get { return movementCurves[index]; }
        
    }
}

[Serializable]
public class MovementCurve 
{
    public AnimationCurve curve;
    public float curveStrength = 10f;
}
