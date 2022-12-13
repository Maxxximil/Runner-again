using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Отслеживание лучшего результата
public class HighScoreController : MonoBehaviour
{
   
    private string _name = "Default";

    //Подписка и отписка на конец игры
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, ChangeHighScore);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, ChangeHighScore);
    }

    //Изменение лучшего счета через менеджер
    private void ChangeHighScore()
    {
        _name = Managers.Auth.GetID();
        Managers.HighScore.AddHighScore(_name);
    }

   
}
