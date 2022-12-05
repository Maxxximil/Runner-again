using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIController : MonoBehaviour
{
    [SerializeField] private Text _scoreCoins;
    [SerializeField] private Text _scoreDistance;
    [SerializeField] private GameObject _scoreScreen;
    [SerializeField] private Text _scoreScreenCoins;
    [SerializeField] private Text _scoreScreenDistance;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _userName;


    private string _name;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.AddListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger<string>.AddListener("USER_NAME", DisplayUser);

    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.RemoveListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
        Messenger<string>.RemoveListener("USER_NAME", DisplayUser);

    }

    private void Start()
    {
        _scoreScreen.SetActive(false);


        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_name);
        OnAddDistance();
        ShowHighScore();
    }

    private void OnAddCoins()
    {
        Managers.Coin.AddCoins(_name, 1);
        _scoreCoins.text = ": " + Managers.Coin.GetCoins(_name);
    }

    private void OnAddDistance()
    {
        _scoreDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    private void OnGameOver()
    {
        _scoreScreen.SetActive(true);
        _scoreScreenCoins.text = "Score: " + Managers.Coin.GetCoins(_name);
        _scoreScreenDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    public void NewGame()
    {
        //Managers.Speed.ChangePause(true);
        //Managers.Speed.UpdateData(0f);
        SceneManager.LoadScene("First");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowHighScore()
    {
        _highScore.text = "Your highscore: " + Managers.HighScore.GetDataScore(_name);
    }

    public void ShowUserName()
    {
        _userName.text = _name;
    }

    private void DisplayUser(string name)
    {
        _name = name;
        ShowHighScore();
        ShowUserName();
    }



}
