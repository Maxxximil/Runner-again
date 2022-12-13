using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Интерфейс от которого наследуются все менеджеры
public interface IGameManager
{
    ManagerStatus status { get; }

    void Startup(NetworkService service);
}
