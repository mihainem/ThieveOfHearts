using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimedItem
{    
    void ProcessTimePassing();

    void ProcessTimeEnded();
}