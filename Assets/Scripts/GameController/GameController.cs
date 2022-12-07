using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] public Transform Player;
    [SerializeField] public Chunk[] ChunkPrefabs;
    [SerializeField] public Chunk FirstChunk;

    

    private List<Chunk> spawnedChanks = new List<Chunk>();


    private void Start()
    {
        Managers.Distance.UpdateData(0);
        spawnedChanks.Add(FirstChunk);
        
    }
    private void Update()
    {
        if(Player.position.z > spawnedChanks[spawnedChanks.Count - 1].End.position.z - 80)
        {
            SpawnChunk();
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

}
