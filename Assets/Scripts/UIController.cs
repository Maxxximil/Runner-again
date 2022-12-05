using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIController : MonoBehaviour
{
    [SerializeField] private Text _scoreDistance;
    [SerializeField] private GameObject _scoreScreen;
    //[SerializeField] private Text _scoreScreenCoins;
    //[SerializeField] private Text _scoreScreenDistance;
    //[SerializeField] private Text _highScore;
    //[SerializeField] private Text _userName;
    //[SerializeField] private Text _scoreCoins;



    //private string _name;
    private void Awake()
    {
        //Messenger.AddListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.AddListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
       // Messenger<string>.AddListener(GameEvent.USER_NAME, DisplayUser);
        Debug.Log("UI Add listeners");
    }

    private void OnDestroy()
    {
       // Messenger.RemoveListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.RemoveListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        //Messenger<string>.RemoveListener(GameEvent.USER_NAME, DisplayUser);


    }

    private void Start()
    {
        _scoreScreen.SetActive(false);
    }

    //private void OnAddCoins()
    //{
    //    Managers.Coin.AddCoins(_name, 1);
    //    _scoreCoins.text = ": " + Managers.Coin.GetCoins(_name);
    //}

    private void OnAddDistance()
    {
        _scoreDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    private void OnGameOver()
    {
        _scoreScreen.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("First");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Reset()
    {
        Managers.Data.NewGame();
    }

    //public void ShowHighScore()
    //{
    //    _highScore.text = "Your highscore: " + _name;//+ Managers.HighScore.GetDataScore(_name);
    //}

    //public void ShowUserName()
    //{
    //    _userName.text = _name;
    //}

    //public void ShowCoins()
    //{
    //    _scoreCoins.text = ": " + _name;//+ Managers.Coin.GetCoins(_name);
    //}

    //private void ShowAll()
    //{
    //    _highScore.text = "Your highscore: " + _name;//+ Managers.HighScore.GetDataScore(_name);
    //    _userName.text = _name;
    //    _scoreCoins.text = ": " + _name;//+ Managers.Coin.GetCoins(_name);

    //}

    //private void DisplayUser(string name)
    //{
    //    _name = name;
    //    Debug.Log("Name: " + _name);
    //    ShowAll();
    //    //ShowHighScore();
    //    //ShowUserName();
    //    //ShowCoins();
    //}



}
