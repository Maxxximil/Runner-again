using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using Firebase;
using System;
using UnityEngine.UI;


public class Facebookauth : MonoBehaviour
{
   
    FirebaseAuth auth;
    public void Awake()
    {
        
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }
    private void InitCallBack()
    {
        if (!FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize");
        }
    }
    private void OnHideUnity(bool isgameshown)
    {
        if (!isgameshown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Facebook_Login()
    {
        var permission = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permission, AuthCallBack);
    }

    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);

            string accesstoken;
            string[] data;
            string acc;
            string[] some;
#if UNITY_EDITOR
            Debug.Log("this is raw access " + result.RawResult);
            data = result.RawResult.Split(',');
            Debug.Log("this is access" + data[3]);
            acc = data[3];
            some = acc.Split('"');
            Debug.Log("this is access " + some[3]);
            accesstoken = some[3];
#elif UNITY_ANDROID
            Debug.Log("this is raw access "+result.RawResult);
 data = result.RawResult.Split(',');
            Debug.Log("this is access"+data[0]);
             acc = data[0];
             some = acc.Split('"');
            Debug.Log("this is access " + some[3]);


             accesstoken = some[3];
#endif
            authwithfirebase(accesstoken);
        }
        else
        {
          Debug.Log("User Cancelled login");
        }
    }
  public void authwithfirebase(string accesstoken)
    {
        auth = FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accesstoken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
               Debug.Log("singin encountered error" + task.Exception);
            }
            Firebase.Auth.FirebaseUser newuser = task.Result;
           Debug.Log(newuser.DisplayName);
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

    }
}
