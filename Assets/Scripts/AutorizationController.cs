using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AutorizationController : MonoBehaviour
{
    [SerializeField] private GameObject _autorizationScreen;
    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private GameObject _registrationScreen;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private OrbitCamera orb;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _userName;
    [SerializeField] private Text _scoreCoins;
    


    private bool _isopenAutorizationScreen = false;
    private bool _isopenLoginScreen = false;
    private bool _isopenRegistrationScreen = false;

    private string _curUser;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.ADD_COINS, OnAddCoins);
        Managers.Data.LoadGameState();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ADD_COINS, OnAddCoins);
       


    }

    





    private void DisplayUser()
    {
        //Debug.Log("HighScore: " + _name);
        _highScore.text = "Your Highscore: " + Managers.HighScore.GetDataScore(Managers.Auth.GetUser());
        Debug.Log("Name: " + _curUser);
        _userName.text = _curUser;
        Debug.Log("Coins: " + _curUser);
        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_curUser);

        //ShowAll(_name);
        //_highScore.text = "Your highscore: " + Managers.HighScore.GetDataScore(_name);
        //_userName.text = name;
        //_scoreCoins.text = ": " + Managers.Coin.GetCoins(_name);
        //ShowHighScore();
        //ShowUserName();
        //ShowCoins();
    }

    private void OnAddCoins()
    {
        Managers.Coin.AddCoins(_curUser, 1);
        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_curUser);
    }

    private void Start()
    {
        _autorizationScreen.SetActive(false);
        _loginScreen.SetActive(false);
        _registrationScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _startScreen.SetActive(true);

        _curUser = Managers.Auth.GetUser();
        DisplayUser();
    }

    private void Update()
    {
        if(_curUser != Managers.Auth.GetUser())
        {
            _curUser = Managers.Auth.GetUser();
            DisplayUser();
        }
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

}
