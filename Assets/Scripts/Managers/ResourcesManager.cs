using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ResourcesManager is a singleton to keep refferences to internal assets
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager instance;
    public static ResourcesManager Instance {
        get
        {
            if (instance == null && !Application.isPlaying)
                instance = GameObject.FindObjectOfType<ResourcesManager>();

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private GameObject _heart;
    public GameObject Heart
    {
        get
        {
            if (_heart == null)
                _heart = Resources.Load<GameObject>("Collectables/BeatingHeart");

            return _heart;
        }
    }



}
