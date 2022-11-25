using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private float _speed;

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Distance manager starting...");

        _network = service;
        _speed = 10f;

        status = ManagerStatus.Started;
    }

    public float GetData()
    {
        return _speed;
    }

    public void UpdateData(float val)
    {
        _speed = val;
    }

    public void AddSpeed(float value)
    {
        _speed += value;
    }
}
