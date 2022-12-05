using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;

public class AuthManager : MonoBehaviour, IGameManager
{
    [SerializeField] private Facebookauth facebookauth;
    [SerializeField] private Login login;
    [SerializeField] private Registration registration;

    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Auth manager starting...");

        _network = service;

        //facebookauth.Launch();
        //registration.Launch();
        //login.Launch();
        
        status = ManagerStatus.Started;
    }

    public void FacebookButton()
    {
        facebookauth.Facebook_Login();
    }
    public void LoginButton()
    {
        login.LogInButton();
    }

    public void SignOut()
    {
        login.OnSignOut();
    }
    public void RegisterButton()
    {
        registration.RegistrarionButton();
    }
}
