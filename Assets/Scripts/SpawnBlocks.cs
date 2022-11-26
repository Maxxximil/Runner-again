using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] public Block[] BlockPrefabs;
    [SerializeField] public Block FirstBlock;

    private List<Block> spawnedBlocks = new List<Block>();

    private int[] posX = { -3, 0, 3 };
    private int posXBlock;


    private void Start()
    {
        spawnedBlocks.Add(FirstBlock);
    }

    private void Update()
    {
        if ((Player.position.z > spawnedBlocks[spawnedBlocks.Count - 1].place.position.z + 5))
        {
            SpawnDiffBlocks();
        }
        //StartCoroutine(CoroSpawnBlocks());

    }

    private void SpawnDiffBlocks()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock = posX[Random.Range(0, posX.Length)];
        Vector3 posBlock = new Vector3(posXBlock, 0.5f, Player.position.z + 20);
        newBlock.transform.position = posBlock;
        spawnedBlocks.Add(newBlock);
        if (spawnedBlocks.Count >= 3)
        {
            DestroyImmediate(spawnedBlocks[0].gameObject, true);
            spawnedBlocks.RemoveAt(0);
        }
    }

    IEnumerator CoroSpawnBlocks()
    {
        Debug.Log("Coro started");
        yield return new WaitForSeconds(5f);
        Debug.Log("Coro finished");
    }
}
