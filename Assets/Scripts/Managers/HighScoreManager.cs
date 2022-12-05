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
        if(_highScore != null)
        {
            return _highScore[name];

        }
        return 0;
    }

    public void UpdateData(Dictionary<string,int> var)
    {
        _highScore = var;
    }

    //public void AddHighScore(Dictionary<string, int> var, string name)
    //{
        
    //    if (_highScore.ContainsKey(name))
    //    {
    //        if (_highScore[name] < var[name])
    //        {
    //            _highScore[name] = var[name];
    //        }
    //    }
    //    else
    //    {
    //        _highScore.Add(name, var[name]);
    //    }
    //    Managers.Data.SaveGameState();
    //}

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

    //public void ShowHighScores(string name)
    //{

    //    if (_highScore.ContainsKey(name))
    //    {
    //        Debug.Log(name + " : " + _highScore[name]);

    //    }


    //}
}
