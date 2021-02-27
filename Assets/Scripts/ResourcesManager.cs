using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager instance;
    public static ResourcesManager Instance { get { return instance;  } }

    private void Awake()
    {
        instance = this;
    }



}
