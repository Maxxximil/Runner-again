using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
public class FirestoreDB : MonoBehaviour
{
    public static FirestoreDB Instance;

    [SerializeField] private string _dbPath = "Data base/One of them";

    FirebaseFirestore db;
    public FirebaseAuth _auth;


    
    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Messenger.AddListener(GameEvent.GAME_OVER, InitializeUser);

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_OVER, InitializeUser);
    }

    public void InitializeUser()
    {
        if (_auth.CurrentUser == null)
        {
            Debug.LogError("UnAutherized user");
            return;
        }
        UserData userData = new UserData
        {
            Name = Managers.Auth.GetUser(),
            ID = Managers.Auth.GetID(),
            Coins = Managers.Coin.GetCoins(Managers.Auth.GetID()),
            HighScore = Managers.HighScore.GetDataScore(Managers.Auth.GetID())
        };
        Debug.Log("Name: " + userData.Name);
        Debug.Log("ID: " + userData.ID);
        Debug.Log("Coins: " + userData.Coins);
        Debug.Log("HighScore: " + userData.HighScore);

        DocumentReference scoreRef = db.Collection("users").Document(_auth.CurrentUser.UserId);
        scoreRef.SetAsync(userData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Initialized user");
        });

    }


    public void GetScore()
    {
        if (_auth.CurrentUser == null)
        {
            Debug.LogError("UnAutherized user");
            return;
        }
        string name;
        string id;
        int coins;
        int highscore;
        db.Collection("users").Document(_auth.CurrentUser.UserId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
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
                name = task.Result.ConvertTo<UserData>().Name;
                id = task.Result.ConvertTo<UserData>().ID;
                coins = task.Result.ConvertTo<UserData>().Coins;
                highscore = task.Result.ConvertTo<UserData>().HighScore;

                Debug.Log("Name = " + name);
                Debug.Log("Id = " + id);
                Debug.Log("Coins = " + coins);
                Debug.Log("high score = " + highscore);

                Managers.Auth.ChangeUser(name);
                Managers.Auth.ChangeID(id);
                Managers.Coin.UpdateCoins(id, coins);
                Managers.HighScore.UpdateHighScore(id, highscore);

                Messenger.Broadcast(GameEvent.SHOW_ALL);
            }
            else
                Debug.Log("Unexpected error");
        });
    }
    //public void GetHighScores()
    //{

    //}
}



[FirestoreData]
public struct UserData
{
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public string ID { get; set; }
    [FirestoreProperty]
    public int Coins { get; set; }

    [FirestoreProperty]

    public int HighScore { get; set; }
}