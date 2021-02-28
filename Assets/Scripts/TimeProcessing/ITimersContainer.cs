using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimersContainer
{
    List<ITimedItem> containedItems { get; set; }
    void Remove(ITimedItem item);
}