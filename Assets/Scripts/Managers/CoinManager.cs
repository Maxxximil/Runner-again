using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    //private int _numCoins { get; set; }
    private Dictionary<string, int> _numCoins;

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Coin manager starting...");

        _network = service;
        UpdateData(new Dictionary<string, int>());

        status = ManagerStatus.Started;
    }

    
    public void AddCoins(string name, int value)
    {

        Managers.Data.LoadGameState();
        if (_numCoins.ContainsKey(name))
        {
            _numCoins[name] += value;
        }
        else
        {
            _numCoins.Add(name, value);
        }
        Messenger.Broadcast(GameEvent.SHOW_ALL);
        Managers.Data.SaveGameState();
    }

    public void UpdateCoins(string id, int value)
    {
        if (_numCoins.ContainsKey(id))
        {
            _numCoins[id] = value;
        }
        else
        {
            _numCoins.Add(id, value);
        }
        Managers.Data.SaveGameState();
    }

    public int GetCoins(string name)
    {
        if (_numCoins.ContainsKey(name))
        {
            return _numCoins[name];
        }
        return 0;
    }

    public Dictionary<string,int> GetData()
    {
        return _numCoins;
    }

    public void UpdateData(Dictionary<string, int> value)
    {
        _numCoins = value;
    }
}
