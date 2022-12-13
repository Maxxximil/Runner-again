using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������������ ������� ����������
public class HighScoreController : MonoBehaviour
{
   
    private string _name = "Default";

    //�������� � ������� �� ����� ����
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_OVER, ChangeHighScore);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, ChangeHighScore);
    }

    //��������� ������� ����� ����� ��������
    private void ChangeHighScore()
    {
        _name = Managers.Auth.GetID();
        Managers.HighScore.AddHighScore(_name);
    }

   
}
