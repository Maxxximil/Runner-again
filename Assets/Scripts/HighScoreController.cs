using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    private Dictionary<string, int> _newHighScore;
    private string _name;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, ChangeHighScore);
        Messenger<string>.AddListener(GameEvent.USER_NAME, ChangeName);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, ChangeHighScore);
        Messenger<string>.RemoveListener(GameEvent.USER_NAME, ChangeName);
    }

    private void Start()
    {
        Managers.Data.LoadGameState();
    }
    private void ChangeHighScore()
    {
        //_newHighScore = new Dictionary<string, int>();
        //_newHighScore.Add(_name, Managers.Distance.GetData());
        Managers.HighScore.AddHighScore(_name);
    }

    private void ChangeName(string name)
    {
        _name = name;
    }
}
