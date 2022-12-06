using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private Dictionary<string, int> _highScore;
    

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("HighScore manager starting...");

        _network = service;
       
        _highScore = new Dictionary<string, int>();

        status = ManagerStatus.Started;
    }

    public Dictionary<string, int> GetData()
    {
        
        return _highScore;
    }

    public int GetDataScore(string name)
    {
        if (_highScore.ContainsKey(name))
        {
            return _highScore[name];

        }
        return 0;
    }

    public void UpdateData(Dictionary<string,int> var)
    {
        _highScore = var;
    }

    public void UpdateHighScore(string id, int value)
    {
        if (_highScore.ContainsKey(id))
        {
            _highScore[id] = value;
        }
        else
        {
            _highScore.Add(id, value);
        }
        Managers.Data.SaveGameState();
    }

    public void AddHighScore(string name)
    {
        Managers.Data.LoadGameState();
        if (_highScore.ContainsKey(name))
        {
            if (_highScore[name] < Managers.Distance.GetData())
            {
                _highScore[name] = Managers.Distance.GetData();
            }
        }
        else
        {
            _highScore.Add(name, Managers.Distance.GetData());
        }
        Managers.Data.SaveGameState();
    }

   
}
