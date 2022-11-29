using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private string _filename;

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Data manager starting...");

        _network = service;

        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();
        gamestate.Add("HighScore", Managers.HighScore.GetData());
        //gamestate.Add("score", Managers.Score.GetDataWonder());

        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }

    public void LoadGameState()
    {
        if (!File.Exists(_filename))
        {
            Debug.Log("No saved game");
            return;
        }

        Dictionary<string, object> gamestate;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        Managers.HighScore.UpdateData((Dictionary<string, int>)gamestate["HighScore"]);
        //Managers.Mission.UpdateData((int)gamestate["curWonder"], (int)gamestate["maxWonder"]);
        //Managers.Mission.RestartCurrent();
    }

    
} 
