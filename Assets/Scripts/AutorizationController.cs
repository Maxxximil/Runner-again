using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AutorizationController : MonoBehaviour
{
    [SerializeField] private GameObject _autorizationScreen;
    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _registrationScreen;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private OrbitCamera orb;

    private bool _isopenAutorizationScreen = false;
    private bool _isopenLoginScreen = false;
    private bool _isopenRegistrationScreen = false;

 

    private void Start()
    {
        _autorizationScreen.SetActive(false);
        _loginScreen.SetActive(false);
        _registrationScreen.SetActive(false);
        _startScreen.SetActive(true);
        _gameScreen.SetActive(false);

    }

   

    public void AutorizationScreen()
    {
        if (!_isopenAutorizationScreen)
        {
            _startScreen.SetActive(false);
            _autorizationScreen.SetActive(true);
            _isopenAutorizationScreen = true;
        }
        else
        {
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _registrationScreen.SetActive(false);
            _isopenRegistrationScreen = false;
            _loginScreen.SetActive(false);
            _isopenLoginScreen = false;
            _startScreen.SetActive(true);
        }
        //Check();

    }

    public void LoginScreen()
    {
        if (!_isopenLoginScreen)
        {
            if (_isopenRegistrationScreen)
            {
                _registrationScreen.SetActive(false);
                _isopenRegistrationScreen = false;
            }
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _loginScreen.SetActive(true);
            _isopenLoginScreen = true;

        }
        else
        {
            _loginScreen.SetActive(false);
            _isopenLoginScreen = false;
            _autorizationScreen.SetActive(true);
            _isopenAutorizationScreen = true;
        }
        //Check();

    }

    public void RegistrationScreen()
    {
        if (!_isopenRegistrationScreen)
        {
            if (_isopenLoginScreen)
            {
                _loginScreen.SetActive(false);
                _isopenLoginScreen = false;
            }
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _registrationScreen.SetActive(true);
            _isopenRegistrationScreen = true;

        }
        else
        {
            _registrationScreen.SetActive(false);
            _isopenRegistrationScreen = false;
            _autorizationScreen.SetActive(true);
            _isopenAutorizationScreen = true;
        }
        //Check();
    }
    
    //private void Check()
    //{
    //    if (_isopenAutorizationScreen || _isopenLoginScreen || _isopenRegistrationScreen)
    //    {
    //        Time.timeScale = 0;
    //    }
    //    else if (!_isopenAutorizationScreen && !_isopenLoginScreen && !_isopenRegistrationScreen)
    //    {
    //        Time.timeScale = 1;
    //    }
    //}

    public void StartButton()
    {
        orb._cameraTrigger = true;
        Managers.Speed.UpdateData(10f);
        Managers.Speed.ChangePause(true);
        _startScreen.SetActive(false);
        _gameScreen.SetActive(true);
    }
    //public void FacebookButton()
    //{
    //    Managers.Auth.FacebookButton();
    //}
    //public void LoginButton()
    //{
    //    Managers.Auth.LoginButton();
    //}

    //public void SignOut()
    //{
    //    Managers.Auth.SignOut();
    //}
    //public void RegisterButton()
    //{
    //    Managers.Auth.RegisterButton();
    //}
}
