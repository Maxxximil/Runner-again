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
    [SerializeField] private string _dbPath = "Data base/One of them";

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    public FirebaseAuth _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

    //public string Name;
    //public string ID;
    //public string Coins;
    //public string HighScore;

    //public void SaveInDataBase()
    //{
    //    var UserData = new UserData
    //    {
    //        Name = Managers.Auth.GetUser(),
    //        ID = Managers.Auth.GetID(),
    //        Coins = Managers.Coin.GetCoins(Managers.Auth.GetUser()),
    //        HighScore = Managers.HighScore.GetDataScore(Managers.Auth.GetUser())

    //    };

    //    var firesore = FirebaseFirestore.DefaultInstance;
    //    firesore.Document(_dbPath).SetAsync(UserData);
    //}


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


        DocumentReference scoreRef = db.Collection("/users").Document(_auth.CurrentUser.UserId);
        scoreRef.SetAsync(userData).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Initialized user");
        });

    }
    //public void UpdateCurrency(int newCurrency)
    //{
    //    if (_auth.CurrentUser == null)
    //    {
    //        Debug.LogError("UnAutherized user");
    //        return;
    //    }
    //    Dictionary<string, object> currency = new Dictionary<string, object>
    //     {
    //         {"currency", newCurrency},
    //     };
    //    DocumentReference currencyRef = db.Collection("users").Document(_auth.CurrentUser.UserId);

    //    currencyRef.UpdateAsync(currency).ContinueWithOnMainThread(task =>
    //    {
    //        Debug.Log("Updated Currency");
    //    });

    //}







    //public void UpdateScore(int newScore)
    //{
    //    if (_auth.CurrentUser == null)
    //    {
    //        Debug.LogError("UnAutherized user");
    //        return;
    //    }
    //    Dictionary<string, object> score = new Dictionary<string, object>
    //     {
    //         {"score", newScore},
    //     };

    //    DocumentReference scoreRef = db.Collection("users").Document(_auth.CurrentUser.UserId);

    //    scoreRef.UpdateAsync(score).ContinueWithOnMainThread(task =>
    //    {
    //        Debug.Log("Updated Score");
    //    });


    //}




    public void GetScore()
    {
        if (_auth.CurrentUser == null)
        {
            Debug.LogError("UnAutherized user");
            return;
        }
        string name;
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
                Debug.Log("user name = " + name);
            }
            else
                Debug.Log("Unexpected error");
        });
    }


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