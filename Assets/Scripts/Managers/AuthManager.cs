using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;

public class AuthManager : MonoBehaviour, IGameManager
{
    private string _curUser;
    private string _curID;

    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Auth manager starting...");

        _network = service;

        _curUser = "Guest";
        _curID = "Guest";

        status = ManagerStatus.Started;
    }

    public string GetUser()
    {
        return _curUser;
    }

    public string GetID()
    {
        return _curID;
    }

    public void ChangeUser(string newName)
    {
        _curUser = newName;
    }

    public void ChangeID(string newID)
    {
        _curID = newID;
    }
   
}
