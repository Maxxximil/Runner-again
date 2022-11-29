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
    [SerializeField] private GameObject _start;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.AddListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ADD_COINS, OnAddCoins);
        Messenger.RemoveListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        _scoreScreen.SetActive(false);
        _start.SetActive(true);
        Managers.Speed.ChangePause(false);
        //Time.timeScale = 0;
        OnAddCoins();
        OnAddDistance();
    }

    private void OnAddCoins()
    {
        _scoreCoins.text = ": " + Managers.Coin.GetCoins();
    }

    private void OnAddDistance()
    {
        _scoreDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    private void OnGameOver()
    {
        _scoreScreen.SetActive(true);
        _scoreScreenCoins.text = "Score: " + Managers.Coin.GetCoins();
        _scoreScreenDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    public void NewGame()
    {
        //Time.timeScale = 1;
        Managers.Speed.ChangePause(true);
        SceneManager.LoadScene("First");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        _start.SetActive(false);
        Managers.Speed.ChangePause(true);
        //Time.timeScale = 1;
    }

}
