using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class FireBaseInit : MonoBehaviour
{
    void Awake()
    {

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Init successful");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}
