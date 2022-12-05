using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField]
    private Vector3 _offset = Vector3.zero;
    [SerializeField]
    private float _smoothTime = 5f;
    private Vector3 _currentVelocity;

    public bool _cameraTrigger { get; set; }

    private void Start()
    {
        _cameraTrigger = false;
    }

    void LateUpdate()
    {
        if (_cameraTrigger)
        {
            transform.LookAt(target);
            transform.position = Vector3.SmoothDamp(transform.position, _offset + target.position, ref _currentVelocity, _smoothTime);
        }
    }
}
