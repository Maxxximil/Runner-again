//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase;
//using Firebase.Auth;
//using Facebook.Unity;

//public class FacebookLogin : MonoBehaviour
//{
//    FirebaseAuth auth;
//    AccessToken accessToken;

//    private void Awake()
//    {
//        if (!FB.IsInitialized)
//        {
//            FB.Init(InitCallBack, OnHideUnity);
//        }
//        else
//        {
//            FB.ActivateApp();
//        }
//    }

//    private void InitCallBack()
//    {
//        if (!FB.IsInitialized)
//        {
//            FB.ActivateApp();
//        }
//        else
//        {
//            Debug.Log("Failed to initialize");
//        }
//    }
//    private void OnHideUnity(bool isgameshown)
//    {
//        if (!isgameshown)
//        {
//            Time.timeScale = 0;
//        }
//        else
//        {
//            Time.timeScale = 1;
//        }
//    }
//    public void FacebookLogIn()
//    {
//        var permission = new List<string>() { "public_profile", "email" };
//        FB.LogInWithReadPermissions(permission, AuthCallBack);
//    }

//    private void AuthCallBack(ILoginResult result)
//    {
//        if (FB.IsLoggedIn)
//        {
//            accessToken = AccessToken.CurrentAccessToken;
//            Debug.Log(accessToken.UserId);
//        }
//        else
//        {
//            Debug.Log("User Cancel login");
//        }
//    }
//    private void Start()
//    {
//        Firebase.Auth.Credential credential =
//    Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);
//        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
//        {
//            if (task.IsCanceled)
//            {
//                Debug.LogError("SignInWithCredentialAsync was canceled.");
//                return;
//            }
//            if (task.IsFaulted)
//            {
//                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
//                return;
//            }

//            Firebase.Auth.FirebaseUser newUser = task.Result;
//            Debug.LogFormat("User signed in successfully: {0} ({1})",
//                newUser.DisplayName, newUser.UserId);
//        });
//    }

//    public void CheckUserFB()
//    {
//        FirebaseUser user = auth.CurrentUser;
//        if (user != null)
//        {
//            string name = user.DisplayName;
//            string email = user.Email;
//            string uid = user.UserId;
//            Debug.Log("Name: " + name + ", Email: " + email + ", UserID: " + uid);
//        }
//    }

//    public void OnSignOut()
//    {
//        auth.SignOut();
//        Debug.Log("Calling SignOut");
//    }
//}
