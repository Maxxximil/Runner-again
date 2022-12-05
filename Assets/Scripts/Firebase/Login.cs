using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using TMPro;



public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputEmail;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private Text warningLoginText;
    [SerializeField] private TMP_InputField _inputEmailForMerge;
    [SerializeField] private TMP_InputField _inputPasswordForMerge;
    [SerializeField] private TMP_InputField _inputNameForMerge;
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    public UnityEvent success;

    public void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        //auth = FirebaseAuth.DefaultInstance;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance; ;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                Managers.Auth.ChangeUser("Guest");
                Managers.Auth.ChangeID("Guest");
                //Messenger<string>.Broadcast(GameEvent.USER_NAME, user.DisplayName);

            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                Debug.Log("Signed in " + user.DisplayName);
                Debug.Log("Signed in " + user.Email);
                Managers.Auth.ChangeUser(user.DisplayName);
                Managers.Auth.ChangeID(user.UserId);

                //Messenger<string>.Broadcast(GameEvent.USER_NAME, user.DisplayName);

            }
        }
    }

    //void OnDestroy()
    //{
    //    auth.StateChanged -= AuthStateChanged;
    //    auth = null;
    //}

    public void OnSignOut()
    {
        auth.SignOut();
        Debug.Log("Calling SignOut");
    }

    public void LogInButton()
    {
        StartCoroutine(LogIn(_inputEmail.text, _inputPassword.text));
    }

    private IEnumerator LogIn(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            warningLoginText.text = "";
            warningLoginText.text = "Logged In";
            Managers.Auth.ChangeUser(user.DisplayName);
            Managers.Auth.ChangeID(user.UserId);
            //Messenger<string>.Broadcast(GameEvent.USER_NAME, user.DisplayName);
            //success.Invoke();
        }
    }

    public void LogInAnon()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            Managers.Auth.ChangeUser("Anon");
            Managers.Auth.ChangeID(newUser.UserId);
        });
    }

    public void MergeAccounts()
    {
        Firebase.Auth.Credential credential =
        Firebase.Auth.EmailAuthProvider.GetCredential(_inputEmailForMerge.text, _inputPasswordForMerge.text);
        auth.CurrentUser.LinkWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("LinkWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("LinkWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Credentials successfully linked to Firebase user: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            UserProfile profile = new UserProfile { DisplayName = _inputNameForMerge.text };
            var ProfileTask = user.UpdateUserProfileAsync(profile);
            Managers.Auth.ChangeID(newUser.UserId);
            Managers.Auth.ChangeUser(newUser.DisplayName);
        });

    }
}
