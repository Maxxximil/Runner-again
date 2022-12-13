using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Создание дороги
public class RoadGenerator : MonoBehaviour
{
    public GameObject RoadPrefab;

    private List<GameObject> roads = new List<GameObject>();

    public float maxSpeed = 7;
    public float speed = 0;
    public int maxRoadCount = 5;

    private void Start()
    {
        ResetLevel();        
    }

    public void StartLevel()
    {
        Managers.Speed.UpdateData(maxSpeed);
    }

    private void Update()
    {
        if (Managers.Speed.GetData() == 0)
        {
            return;
        }
        //Движение дороги
        foreach(GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, Managers.Speed.GetData() * Time.deltaTime);
        }
        //Удаление и создание дороги
        if (roads[0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }
        
    }



    //Обновление уровня
    public void ResetLevel()
    {
        Managers.Speed.UpdateData(0);
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }

        for(int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }

    //Создание дороги
    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0)
        {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15);
        }
        GameObject go  = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

}
