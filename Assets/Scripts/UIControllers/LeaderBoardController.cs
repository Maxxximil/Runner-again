using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;


//Таблица рекордов с БД
public class LeaderBoardController : MonoBehaviour
{
    public static LeaderBoardController Instanse;


    [SerializeField] private Text[] _scores;
    FirebaseFirestore db;
    private List<DocumentSnapshot> _highscoresList;

    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        
        
        //GetHighScores();
        
    }

    
    //Получение всех рекордов с БД
    public void GetHighScores()
    {
        _highscoresList = new List<DocumentSnapshot>();
        db.Collection("users").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("GetUserData was canceled.");
            }

            else if (task.IsFaulted)
            {
                Debug.LogError("GetUserData encountered an error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                var count = task.Result.Count;
                for (int i = 0; i < count; i++)
                {
                    _highscoresList.Add(task.Result[i]);
                    

                }
                SortList();
            }
            else
                Debug.Log("Unexpected error");
        });
        
    }
    //Сортировка
    private void SortList()
    {
        for(int i = 0; i < _highscoresList.Count; i++)
        {
            
            var key = _highscoresList[i];
            var j = i;
            
            while ((j > 0) && (_highscoresList[j - 1].ConvertTo<UserData>().HighScore < key.ConvertTo<UserData>().HighScore))
            {
                var temp = _highscoresList[j - 1];
                _highscoresList[j - 1] = _highscoresList[j];
                _highscoresList[j] = temp;
                j--;
            }
            _highscoresList[j] = key;
        }
        ShowList();
    }

    //Вывод на экран всех рекордов
    private void ShowList()
    {
       

        for (int i = 0; i < _scores.Length && i < _highscoresList.Count; i++)
        {
            _scores[i].text = (i + 1) + ". " + _highscoresList[i].ConvertTo<UserData>().Name + 
                " : " + _highscoresList[i].ConvertTo<UserData>().HighScore;
        }


    }


}

