using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;

//Инициализация Firebase 

public class FireBaseInit : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }


    public static bool firebaseReady;
    //Запуск инициализации
    public void Startup(NetworkService networkService)
    {
        CheckIfReady();
    }

    void Update()
    {
        if (firebaseReady == true)
        {
            status = ManagerStatus.Started;
        }
    }
    //Инициализация
    public static void CheckIfReady()
    {
        Debug.Log("Firebase is starting dependencies.");

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                firebaseReady = true;
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
