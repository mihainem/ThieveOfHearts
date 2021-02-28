using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform target;
    private Transform _transform;
    private Vector3 minLimit;
    private Vector3 maxLimit;

    Vector3 temp;

    private void Awake()
    {
        _transform = this.transform;
        minLimit = new Vector3(-Mathf.Infinity, -Mathf.Infinity, _transform.position.z);
        maxLimit = new Vector3(Mathf.Infinity, Mathf.Infinity, _transform.position.z);
    }
    private void LateUpdate()
    {
        temp = target.position;
        _transform.position = new Vector3(
            Mathf.Clamp(temp.x, minLimit.x, maxLimit.x),
            Mathf.Clamp(temp.y, minLimit.y, maxLimit.y),
            _transform.position.z
            );
    }

    public void SetLimits(Vector3 minLimit, Vector3 maxLimit) 
    {
        this.minLimit = minLimit;
        this.maxLimit = maxLimit;
    }
}
