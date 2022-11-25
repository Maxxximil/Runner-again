using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private int _numCoins { get; set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Coin manager starting...");

        _network = service;
        _numCoins = 0;

        status = ManagerStatus.Started;
    }

    
    public void AddCoins(int value)
    {
        _numCoins += value;
    }

    public int GetCoins()
    {
        return _numCoins;
    }

    public void UpdateData(int value)
    {
        _numCoins = value;
    }
}
