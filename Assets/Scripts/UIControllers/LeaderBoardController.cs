using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField] private Text[] _scores;
    FirebaseFirestore db;
    private List<DocumentSnapshot> _highscoresList;
    private void Awake()
    {
        db = FirebaseFirestore.DefaultInstance;
        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;


    }
    private void Start()
    {
        _highscoresList = new List<DocumentSnapshot>();
        //_leaders = Managers.HighScore.GetData();
        //foreach(var element in _leaders)
        //{
        //    Debug.Log("Name: " + element.Key + ". HighScore: " + element.Value);
        //}
        GetHighScores();
        //ShowList();
        //ShowHighScore();
    }

    //private void GetHighScores()
    //{
    //    FirebaseDatabase.DefaultInstance
    //  .GetReference("users").Child(Managers.Auth.GetID()).Child("Name")
    //  .GetValueAsync().ContinueWithOnMainThread(task =>
    //  {
    //      if (task.IsFaulted)
    //      {
    //          // Handle the error...
    //      }
    //      else if (task.IsCompleted)
    //      {
    //          DataSnapshot snapshot = task.Result;
    //          var temp = snapshot./*Child("Name").*/Value.ToString();
    //          Debug.Log("Snapshot " + temp);
    //      }
    //  });
    //}

    private void GetHighScores()
    {

        db.Collection("users")/*.Document(Managers.Auth.GetID())*/.GetSnapshotAsync().ContinueWithOnMainThread(task =>
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
                //var temp = task.Result[0];
                //Debug.Log("Snapshot: " + temp);
                var count = task.Result.Count;
                for (int i = 0; i < count; i++)
                {
                    _highscoresList.Add(task.Result[i]);
                    //highscore = task.Result[i].ConvertTo<UserData>().HighScore;
                    //ID = task.Result[i].ConvertTo<UserData>().ID;
                    //Debug.Log("ID: " + _highscoresList[i].ConvertTo<UserData>().ID + " HighScore: " + _highscoresList[i].ConvertTo<UserData>().HighScore);

                    //if (_leaders.ContainsKey(ID))
                    //{
                    //    Debug.Log("ContainsKey");
                    //    _leaders[ID] = highscore;
                    //}
                    //else
                    //{
                    //    Debug.Log(" not ContainsKey");
                    //    _leaders.Add(ID, highscore);
                    //}
                    //AddNewHighScore(ID, highscore);

                }
                SortList();

                //var temp = task.Result
                //Debug.Log("Snapshot " + highscore);
            }
            else
                Debug.Log("Unexpected error");
        });
        
    }

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

    private void ShowList()
    {
       

        for (int i = 0; i < _scores.Length && i < _highscoresList.Count; i++)
        {
            _scores[i].text = (i + 1) + ". " + _highscoresList[i].ConvertTo<UserData>().Name + 
                " : " + _highscoresList[i].ConvertTo<UserData>().HighScore;
            //Debug.Log("ID: " + _highscoresList[i].ConvertTo<UserData>().ID + " HighScore: " + _highscoresList[i].ConvertTo<UserData>().HighScore);
        }


    }


}

