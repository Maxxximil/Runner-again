using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;

public class AuthManager : MonoBehaviour, IGameManager
{
    private string _curUser;

    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Auth manager starting...");

        _network = service;

        _curUser = "Default";

        status = ManagerStatus.Started;
    }

    public string GetUser()
    {
        return _curUser;
    }

    public void ChangeUser(string newName)
    {
        _curUser = newName;
        Debug.Log("AuthManager");
        //Messenger.Broadcast(GameEvent.USER_NAME);
    }

   
}
