using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] public Transform Player;
    [SerializeField] public Chunk[] ChunkPrefabs;
    [SerializeField] public Chunk FirstChunk;
    //[SerializeField] public Block[] BlockPrefabs;
    //[SerializeField] public Block FirstBlock;
    [SerializeField] public CollectebleItems[] collectebles;
    [SerializeField] public CollectebleItems FirstColl;

    private List<Chunk> spawnedChanks = new List<Chunk>();
    //private List<Block> spawnedBlocks = new List<Block>();
    private List<CollectebleItems> spawnedColl = new List<CollectebleItems>();

    private int[] posX = { -3, 0, 3 };
    private int posXBlock = 0;
    private int posXColl;

    private void Start()
    {
        Managers.Coin.UpdateData(0);
        Managers.Distance.UpdateData(0);
        Managers.Speed.UpdateData(10);
        spawnedChanks.Add(FirstChunk);
        //spawnedBlocks.Add(FirstBlock);
        spawnedColl.Add(FirstColl);
    }
    private void Update()
    {
        if(Player.position.z > spawnedChanks[spawnedChanks.Count - 1].End.position.z - 80)
        {
            SpawnChunk();
        }


    }
    private void FixedUpdate()
    {
        if (Random.Range(0, 100) == 1)
        {
            SpawnColletbiles();
        }
    }
    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChanks[spawnedChanks.Count-1].End.position - newChunk.Begin.position;
        spawnedChanks.Add(newChunk);
        if(spawnedChanks.Count >= 3)
        {
            DestroyImmediate(spawnedChanks[0].gameObject, true);
            spawnedChanks.RemoveAt(0);
        }
    }


    private void SpawnColletbiles()
    {
        CollectebleItems newColl = Instantiate(collectebles[Random.Range(0, collectebles.Length)]);
        posXColl = posX[Random.Range(0, posX.Length)];
        Vector3 posColl;
        if(posXColl != posXBlock)
        {
            posColl = new Vector3(posXColl, 1, Player.position.z + 20);
        }
        else
        {
            posColl = new Vector3(posXColl, 3, Player.position.z + 20);
        }
        if (posColl != null)
        {
            newColl.transform.position = posColl;
            spawnedColl.Add(newColl);
        }
        if (spawnedColl.Count >= 10)
        {
            if (spawnedColl[0] == null)
            {
                spawnedColl.RemoveAt(0);
            }
            if (spawnedColl[0] != null)
            {
                DestroyImmediate(spawnedColl[0].gameObject, true);
                spawnedColl.RemoveAt(0);
            }
        }
    }
}
