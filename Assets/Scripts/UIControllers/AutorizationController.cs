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
    [SerializeField] private GameObject _mergeScreen;
    [SerializeField] private GameObject _shopScreen;
    [SerializeField] private GameObject _leaderBoardScreen;
    [SerializeField] private OrbitCamera orb;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _userName;
    [SerializeField] private Text _scoreCoins;
    


    private bool _isopenAutorizationScreen = false;
    private bool _isopenLoginScreen = false;
    private bool _isopenRegistrationScreen = false;
    private bool _isopenMergeScreen = false;
    private bool _isopenShopScreen = false;
    private bool _isopenLeaderBoardScreen = false;

    private string _curUser;
    private string _curID;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.AddListener(GameEvent.SHOW_ALL, DisplayUser);
        Managers.Data.LoadGameState();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.RemoveListener(GameEvent.SHOW_ALL, DisplayUser);

    }


    private void DisplayUser()
    {       
        _highScore.text = "Your Highscore: " + Managers.HighScore.GetDataScore(_curID);
        Debug.Log("Name: " + _curUser);
        _userName.text = Managers.Auth.GetUser();
        Debug.Log("Coins: " + Managers.Coin.GetCoins(_curID));
        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_curID);
    }

    private void OnAddCoins()
    {
        Managers.Coin.AddCoins(_curID, 1);
        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_curID);
    }

    private void Start()
    {
        _autorizationScreen.SetActive(false);
        _loginScreen.SetActive(false);
        _registrationScreen.SetActive(false);
        _gameScreen.SetActive(false);
        _startScreen.SetActive(true);
        _mergeScreen.SetActive(false);
        _shopScreen.SetActive(false);
        _leaderBoardScreen.SetActive(false);

        _curUser = Managers.Auth.GetUser();
        _curID = Managers.Auth.GetID();
        DisplayUser();
    }

    private void Update()
    {
        if(_curID != Managers.Auth.GetID())
        {
            _curUser = Managers.Auth.GetUser();
            _curID = Managers.Auth.GetID();
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
            _shopScreen.SetActive(false);
            _isopenShopScreen = false;
            _leaderBoardScreen.SetActive(false);
            _isopenLeaderBoardScreen = false;
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
    }

    public void ShopScreen()
    {
        if (!_isopenShopScreen)
        {
            _startScreen.SetActive(false);
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _shopScreen.SetActive(true);
            _isopenShopScreen = true;
            _leaderBoardScreen.SetActive(false);
            _isopenLeaderBoardScreen = false;
        }
        else
        {
            _startScreen.SetActive(true);
            _shopScreen.SetActive(false);
            _isopenShopScreen = false;
        }
    }

    public void LeaderBoardScreen()
    {
        if(!_isopenLeaderBoardScreen)
        {
            _startScreen.SetActive(false);
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _shopScreen.SetActive(false);
            _isopenShopScreen = false;
            _leaderBoardScreen.SetActive(true);
            _isopenLeaderBoardScreen = true;
        }
        else
        {
            _leaderBoardScreen.SetActive(false);
            _isopenLeaderBoardScreen = false;
            _startScreen.SetActive(true);

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

    public void MergeScreen()
    {
        if (!_isopenMergeScreen)
        {
            _autorizationScreen.SetActive(false);
            _isopenAutorizationScreen = false;
            _mergeScreen.SetActive(true);
            _isopenMergeScreen = true;
        }
        else
        {
            _autorizationScreen.SetActive(true);
            _isopenAutorizationScreen = true;
            _mergeScreen.SetActive(false);
            _isopenMergeScreen = false;
        }
    }

    
    

    public void StartButton()
    {
        orb._cameraTrigger = true;
        Managers.Speed.UpdateData(10f);
        Managers.Speed.ChangePause(true);
        _startScreen.SetActive(false);
        _gameScreen.SetActive(true);
    }
}
