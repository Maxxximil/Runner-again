using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] public Transform Player;


    private int _itemSpace = 10;
    private int _itemCountInMap = 5;
    private int[] posX = { -3, 0, 3 };
    public  int LaneOffset = 3;
    private int _coinsCountInItem = 10;
    private float _coinsHeight = 1.2f;
    private int _mapSize;
    enum TrackPos { Left = -1, Center = 0, Right = 1};

    enum CoinsStyle { Line, Jump };

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
    public GameObject CoinPrefab;

    public List<GameObject> maps = new List<GameObject>();
    public List<GameObject> activeMaps = new List<GameObject>();

    private void Start()
    {
        _mapSize = _itemCountInMap * _itemSpace;
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());
        maps.Add(MakeMap1());
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

        foreach(GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, Managers.Speed.GetData() * Time.deltaTime);
            //map.transform.position = new Vector3(0, 0, Player.position.z);
        }
        if (activeMaps[0].transform.position.z < -_mapSize)
        {
            RemoveFirstActiveMap();
            AddActiveMap();
        }
    }

    private void RemoveFirstActiveMap()
    {
        activeMaps[0].SetActive(false);
        maps.Add(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }

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

    private GameObject MakeMap1()
    {
        GameObject result = new GameObject("Map1");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for(int i = 0; i< _itemCountInMap; i++)
        {
            item.SetValues(null, TrackPos.Center, CoinsStyle.Line);

            if (i == 1)
            {
                item.SetValues(BlockTopPrefab, TrackPos.Left, CoinsStyle.Line);
            }

            if (i == 2)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
            }
            else if (i == 3)
            {
                item.SetValues(BlockTopPrefab, TrackPos.Left, CoinsStyle.Line);
            }
            else if (i == 4)
            {
                item.SetValues(BlockBottomPrefab, TrackPos.Right, CoinsStyle.Jump);
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

    void CreateCoins(CoinsStyle style, Vector3 pos, GameObject parentObject)
    {
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
