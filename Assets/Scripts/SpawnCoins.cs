using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] public CollectebleItems[] coins;
    [SerializeField] public CollectebleItems FirstCoin;

    private List<CollectebleItems> spawnedCoin = new List<CollectebleItems>();

    private int[] posX = { -3, 0, 3 };
    private float[] posY = { 1.5f, 3 };

    public int spawnDistance = 20;


    private int posXColl = 0;
    private float posYColl = 3;

    

    private void Start()
    { 
        spawnedCoin.Add(FirstCoin);
    }

    private void Update()
    {
        if ((Player.position.z > spawnedCoin[spawnedCoin.Count - 1].place.position.z - 10))
        {
            SpawnDiffCoins();
        }
        if (spawnedCoin.Count >= 10)
        {
            if (spawnedCoin[0] == null)
            {
                spawnedCoin.RemoveAt(0);
            }
            if (spawnedCoin[0] != null)
            {
                DestroyImmediate(spawnedCoin[0].gameObject, true);
                spawnedCoin.RemoveAt(0);
            }
        }
    }

    private void SpawnDiffCoins()
    {
        CollectebleItems newCoin1 = Instantiate(coins[Random.Range(0, coins.Length)]);
        CollectebleItems newCoin2 = Instantiate(coins[Random.Range(0, coins.Length)]);
        CollectebleItems newCoin3 = Instantiate(coins[Random.Range(0, coins.Length)]);
        CollectebleItems newCoin4 = Instantiate(coins[Random.Range(0, coins.Length)]);
        posXColl = posX[Random.Range(0, posX.Length)];
        posYColl = posY[Random.Range(0, posY.Length)];
        
        Vector3 posCoin1 = new Vector3(posXColl, posYColl, Player.position.z + 20);
        Vector3 posCoin2 = new Vector3(posXColl, posYColl, Player.position.z + 25);
        Vector3 posCoin3 = new Vector3(posXColl, posYColl, Player.position.z + 30);
        Vector3 posCoin4 = new Vector3(posXColl, posYColl, Player.position.z + 35);
        newCoin1.transform.position = posCoin1;
        newCoin2.transform.position = posCoin2;
        newCoin3.transform.position = posCoin3;
        newCoin4.transform.position = posCoin4;
        spawnedCoin.Add(newCoin1);
        spawnedCoin.Add(newCoin2);
        spawnedCoin.Add(newCoin3);
        spawnedCoin.Add(newCoin4);      
    }


}
