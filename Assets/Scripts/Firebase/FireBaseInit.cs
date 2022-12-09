using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;

public class FireBaseInit : MonoBehaviour, IGameManager
{
    //void Start()
    //{
    //    Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
    //        var dependencyStatus = task.Result;
    //        if (dependencyStatus == Firebase.DependencyStatus.Available)
    //        {
    //            FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
    //            Debug.Log("Init successful");
    //        }
    //        else
    //        {
    //            UnityEngine.Debug.LogError(System.String.Format(
    //              "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    //        }
    //    });
    //}

    public ManagerStatus status { get; private set; }


    public static bool firebaseReady;

    public void Startup(NetworkService networkService)
    {
        CheckIfReady();
    }

    void Update()
    {
        if (firebaseReady == true)
        {

            //SceneManager.LoadScene("LoginScene");

            status = ManagerStatus.Started;

        }
    }

    public static void CheckIfReady()
    {
        Debug.Log("Firebase is starting dependencies.");

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                firebaseReady = true;
                //Messenger.Broadcast(StartupEvent.FIREBASE_READY);
                Debug.Log("Firebase is ready for use.");
            }
            else
            {
                firebaseReady = false;
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
        
    }
}
