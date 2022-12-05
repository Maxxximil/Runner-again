using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private float _speed;
    private bool _isPaused;

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Distance manager starting...");

        _network = service;
        _isPaused = false;
        _speed = 0;

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

    public void ChangePause( bool var)
    {
        _isPaused = var;
        if (_isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
