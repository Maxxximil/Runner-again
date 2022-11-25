using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private int _distance;

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Distance manager starting...");

        _network = service;
        _distance = 1;

        status = ManagerStatus.Started;
    }

    public int GetData()
    {
        return _distance;
    }

    public void UpdateData(int val)
    {
        _distance = val;
    }

    public void AddDistance(int value)
    {
        _distance += value;
    }
}
