using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private float _speed;
    public float time = 0.1f;
    private int _distance;
    private int _prevDistance;

    private void Start()
    {
        _distance = Managers.Distance.GetData();
        _speed = Managers.Speed.GetData();
        _prevDistance = _distance;
    }

    private void FixedUpdate()
    {
        _speed = Managers.Speed.GetData();
        if(_speed != 0)
        {         
            _speed = Managers.Speed.GetData();
            _distance = (int)(_speed * time);
            Managers.Distance.AddDistance(_distance);
            Messenger.Broadcast("ADD_DISTANCE");
        }
        if(Managers.Distance.GetData() > _prevDistance + 600)
        {
            Managers.Speed.AddSpeed(1f);
            _prevDistance = Managers.Distance.GetData();
        }
    }

}
