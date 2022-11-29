using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutorizationController : MonoBehaviour
{
    [SerializeField] private GameObject _autorizationScreen;
    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _registrationScreen;

    private bool _isopenAutorizationScreen = false;
    private bool _isopenLoginScreen = false;
    private bool _isopenRegistrationScreen = false;


    private void Start()
    {
        _autorizationScreen.SetActive(false);
        _loginScreen.SetActive(false);
        _registrationScreen.SetActive(false);
    }

    private void Update()
    {
        if (_isopenAutorizationScreen || _isopenLoginScreen || _isopenRegistrationScreen)
        {
            Time.timeScale = 0;
        }
        else if (Managers.Speed.GetData() != 0) 
        {
            Time.timeScale = 1;
        }
    }

    public void AutorizationScreen()
    {
        if (!_isopenAutorizationScreen)
        {
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
        }
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
    }

}
