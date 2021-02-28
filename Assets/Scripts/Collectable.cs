using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public void FlyToRecipient()
    {
        GameManager.Instance.ProcessCollecting(this);
    }

    public void UpdateCounter()
    {
      //  Debug.Log("Updating Counter");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FlyToRecipient();
        UpdateCounter();
    }
}
