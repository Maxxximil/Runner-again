using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Новая игра и конец игры

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _scoreDistance;
    [SerializeField] private GameObject _scoreScreen;
    
    //Подписка и отписка от событий конца игры и добавление расстояний
    private void Awake()
    {        
        Messenger.AddListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.AddListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ADD_DISTANCE, OnAddDistance);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        _scoreScreen.SetActive(false);
    }

    //Вывод на экран пройженного расстояния
    private void OnAddDistance()
    {
        _scoreDistance.text = "Distance: " + Managers.Distance.GetData();
    }

    //Конец игры, вывод окна с результатом
    private void OnGameOver()
    {
        _scoreScreen.SetActive(true);
    }

    //Новая игра
    public void NewGame()
    {
        SceneManager.LoadScene("First");
        Managers.Distance.UpdateData(1);
    }

    //Выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }


    //Сброс всех данных на локальном файле
    public void Reset()
    {
        Managers.Data.NewGame();
    }
}
