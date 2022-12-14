using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Генератор карты
public class MapGenerator : MonoBehaviour
{
    [SerializeField] public Transform Player;


    private int _itemSpace = 10;
    private int _itemCountInMap = 10;
    public  int LaneOffset = 3;
    private int _coinsCountInItem = 6;
    private float _coinsHeight = 1f;
    private int _mapSize;
    enum TrackPos { Left = -1, Center = 0, Right = 1};

    enum CoinsStyle { Line, Jump, None };

    //Один участок карты
    struct MapItem
    {
        public void SetValues(GameObject gameObject, TrackPos track, CoinsStyle coinsStyle)
        {
            this.obstacle = gameObject;
            this.trackPos = track;
            this.coinsStyle = coinsStyle;
        }
        public GameObject obstacle;
        public TrackPos trackPos;
        public CoinsStyle coinsStyle;
    }

    public GameObject BlockTopPrefab;
    public GameObject BlockBottomPrefab;
    public GameObject BlocksBotUpPrefab;
    public GameObject BlocksBotBotPrefab;
    public GameObject BlocksUpBotPrefab;
    public GameObject BlocksUpUpPrefab;
    public GameObject BlocksBotBotBotPrefab;
    public GameObject BlocksBotBotUpPrefab;
    public GameObject BlocksBotUpBotPrefab;
    public GameObject BlocksBotUpUpPrefab;
    public GameObject BlocksUpBotBotPrefab;
    public GameObject BlocksUpBotUpPrefab;
    public GameObject BlocksUpUpBotPrefab;
    public GameObject BlocksUpUpUpPrefab;
    
      
    public GameObject CoinPrefab;

    public List<GameObject> maps = new List<GameObject>();
    public List<GameObject> activeMaps = new List<GameObject>();

    private void Start()
    {
        _mapSize = _itemCountInMap * _itemSpace;
        //Заполнение массива картами
        maps.Add(MakeMap1());
        maps.Add(MakeMap2());
        maps.Add(MakeMap3());
        //Добоавление активных карт
        AddActiveMap();
        AddActiveMap();
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }

    private void Update()
    {
        if(Managers.Speed.GetData() == 0)
        {
            return;
        }
        //Перемещение карт
        foreach(GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, Managers.Speed.GetData() * Time.deltaTime);
        }
        //Удаление и создание новых карт
        if (activeMaps[0].transform.position.z < -_mapSize)
        {
            RemoveFirstActiveMap();
            AddActiveMap();
        }
    }

    //Удаление первой из активных карт
    private void RemoveFirstActiveMap()
    {
        activeMaps[0].SetActive(false);
        maps.Add(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }

    //Добавление активных карт из общего пула
    void AddActiveMap()
    {
        int r = Random.Range(0, maps.Count);
        GameObject go = maps[r];
        go.SetActive(true);
        foreach(Transform child in go.transform)
        {
            child.gameObject.SetActive(true);
        }
        go.transform.position = activeMaps.Count > 0 ? 
            activeMaps[activeMaps.Count - 1].transform.position + Vector3.forward * _mapSize :
            new Vector3(0, 0, 20);
        maps.RemoveAt(r);
        activeMaps.Add(go);
    }

    //Создание разных карт

    private GameObject MakeMap1()
    {
        GameObject result = new GameObject("Map1");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for(int i = 0; i< _itemCountInMap; i++)
        {
            if(i == 0)
            {
                item.SetValues(BlocksBotBotPrefab, TrackPos.Center, CoinsStyle.Jump);
            }

            if (i == 1)
            {
                item.SetValues(BlockTopPrefab, TrackPos.Left, CoinsStyle.Line);
            }

            if (i == 2)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Left, CoinsStyle.None);
            }
            if (i == 3)
            {
                item.SetValues(BlocksUpBotUpPrefab, TrackPos.Center, CoinsStyle.Jump);
            }
            if (i == 4)
            {
                item.SetValues(BlocksBotBotUpPrefab, TrackPos.Center, CoinsStyle.Line   );
            }
            if (i == 5)
            {
                item.SetValues(BlocksUpUpPrefab, TrackPos.Center, CoinsStyle.Line);
            }
            if (i == 6)
            {
                item.SetValues(BlocksUpBotPrefab, TrackPos.Left, CoinsStyle.None);
            }
            if (i == 7)
            {
                item.SetValues(BlocksBotBotBotPrefab, TrackPos.Center, CoinsStyle.Jump);
            }
            if (i == 8)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 9)
            {
                item.SetValues(BlockTopPrefab, TrackPos.Left, CoinsStyle.Line);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos * LaneOffset, 0.5f, i * _itemSpace);
            
            if (item.obstacle != null)
            {
                CreateCoins(item.coinsStyle, obstaclePos, result);
                GameObject go = Instantiate(item.obstacle);
                go.transform.position = obstaclePos;
                go.transform.SetParent(result.transform);
            }
        }
        
        return result;
    }


    private GameObject MakeMap2()
    {
        GameObject result = new GameObject("Map2");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for (int i = 0; i < _itemCountInMap; i++)
        {
            if (i == 0)
            {
                item.SetValues(BlocksBotBotBotPrefab, TrackPos.Center, CoinsStyle.Jump);
            }

            if (i == 1)
            {
                item.SetValues(BlocksUpUpPrefab, TrackPos.Center, CoinsStyle.Line);
            }

            if (i == 2)
            {
                item.SetValues(BlocksBotUpUpPrefab, TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 3)
            {
                item.SetValues(BlocksUpBotUpPrefab, TrackPos.Center, CoinsStyle.Jump);
            }
            if (i == 4)
            {
                item.SetValues(BlocksBotBotUpPrefab, TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 5)
            {
                item.SetValues(BlocksBotUpPrefab, TrackPos.Center, CoinsStyle.Jump);
            }
            if (i == 6)
            {
                item.SetValues(BlocksUpBotPrefab, TrackPos.Left, CoinsStyle.None);
            }
            if (i == 7)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 8)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 9)
            {
                item.SetValues(null, TrackPos.Left, CoinsStyle.Line);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos * LaneOffset, 0.5f, i * _itemSpace);

            if (item.obstacle != null)
            {
                CreateCoins(item.coinsStyle, obstaclePos, result);
                GameObject go = Instantiate(item.obstacle);
                go.transform.position = obstaclePos;
                go.transform.SetParent(result.transform);
            }
        }

        return result;
    }
    private GameObject MakeMap3()
    {
        GameObject result = new GameObject("Map3");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for (int i = 0; i < _itemCountInMap; i++)
        {
            if (i == 0)
            {
                item.SetValues(BlocksUpBotUpPrefab, TrackPos.Center, CoinsStyle.Jump);
            }

            if (i == 1)
            {
                item.SetValues(BlocksUpBotBotPrefab, TrackPos.Center, CoinsStyle.None);
            }

            if (i == 2)
            {
                item.SetValues(BlocksBotUpPrefab, TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 3)
            {
                item.SetValues(BlocksUpBotPrefab, TrackPos.Center, CoinsStyle.Line);
            }
            if (i == 4)
            {
                item.SetValues(BlocksBotBotUpPrefab, TrackPos.Center, CoinsStyle.None);
            }
            if (i == 5)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 6)
            {
                item.SetValues(BlocksUpBotPrefab, TrackPos.Left, CoinsStyle.None);
            }
            if (i == 7)
            {
                item.SetValues(BlocksUpUpUpPrefab, TrackPos.Center, CoinsStyle.Line);
            }
            if (i == 8)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 9)
            {
                item.SetValues(BlockTopPrefab, TrackPos.Left, CoinsStyle.Line);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos * LaneOffset, 0.5f, i * _itemSpace);

            if (item.obstacle != null)
            {
                CreateCoins(item.coinsStyle, obstaclePos, result);
                GameObject go = Instantiate(item.obstacle);
                go.transform.position = obstaclePos;
                go.transform.SetParent(result.transform);
            }
        }

        return result;
    }

    //Создание монет для карты
    void CreateCoins(CoinsStyle style, Vector3 pos, GameObject parentObject)
    {
        if (style == CoinsStyle.None) return;
        Vector3 coinPos = Vector3.zero;
        if(style == CoinsStyle.Line)
        {
            for(int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
            {
                coinPos.y = _coinsHeight;
                coinPos.z = i*((float)_itemSpace/_coinsCountInItem);
                GameObject go = Instantiate(CoinPrefab);
                go.transform.position = coinPos + pos;
                go.transform.SetParent(parentObject.transform);
            }
        }
        if (style == CoinsStyle.Jump)
        {
            for (int i = -_coinsCountInItem / 2; i < _coinsCountInItem / 2; i++)
            {
                coinPos.y = Mathf.Max(-1 / 2f * Mathf.Pow(i, 2) + 3, _coinsHeight);
                coinPos.z = i * ((float)_itemSpace / _coinsCountInItem);
                GameObject go = Instantiate(CoinPrefab);
                go.transform.position = coinPos + pos;
                go.transform.SetParent(parentObject.transform);
            }
        }
    }
}
