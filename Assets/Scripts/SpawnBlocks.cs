using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] public Block[] BlockPrefabs;
    [SerializeField] public Block FirstBlock;

    public int spawnDistance = 20;

    private List<Block> spawnedBlocks = new List<Block>();

    private int[] posX = { -3, 0, 3 };
    private int posXBlock1;
    private int posXBlock3;
    private int posXBlock2;


    private void Start()
    {
        spawnedBlocks.Add(FirstBlock);
    }

    private void Update()
    {
        if ((Player.position.z > spawnedBlocks[spawnedBlocks.Count - 1].place.position.z - 10))
        {
            SpawnDiffBlocks();
        }
        if (spawnedBlocks.Count >= 20)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(j);
                DestroyImmediate(spawnedBlocks[j].gameObject, true);
                spawnedBlocks.RemoveAt(j);
            }
        }
    }

    private void SpawnDiffBlocks()
    {
        int i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                SpawnOneBlockNear();
                break;
            case 2:
                SpawnTwoBlocksNear();
                break;
            case 3:
                SpawnThreeBlocksNear();
                break;
            default:
                break;
        }
        i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                SpawnOneBlockMiddle();
                break;
            case 2:
                SpawnTwoBlocksMiddle();
                break;
            case 3:
                SpawnThreeBlocksMiddle();
                break;
            default:
                break;
        }
        i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                SpawnOneBlockFar();
                break;
            case 2:
                SpawnTwoBlocksFar();
                break;
            case 3:
                SpawnThreeBlocksFar();
                break;
            default:
                break;
        }

    }

    private void SpawnOneBlockNear()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        Vector3 posBlock = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance);
        newBlock.transform.position = posBlock;
        spawnedBlocks.Add(newBlock);
    }

    private void SpawnTwoBlocksNear()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        do
        {
            posXBlock2 = posX[Random.Range(0, posX.Length)];
        }while(posXBlock1 == posXBlock2);
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance);
        newBlock.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        spawnedBlocks.Add(newBlock);
        spawnedBlocks.Add(newBlock2);
    }

    private void SpawnThreeBlocksNear()
    {
        Block newBlock1 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock3 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[0];
        posXBlock2 = posX[1];
        posXBlock3 = posX[2];
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance);
        Vector3 posBlock3= new Vector3(posXBlock3, 0.5f, Player.position.z + spawnDistance);
        newBlock1.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        newBlock3.transform.position = posBlock3;
        spawnedBlocks.Add(newBlock1);
        spawnedBlocks.Add(newBlock2);
        spawnedBlocks.Add(newBlock3);
    }
    private void SpawnOneBlockMiddle()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        Vector3 posBlock = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 2);
        newBlock.transform.position = posBlock;
        spawnedBlocks.Add(newBlock);
    }

    private void SpawnTwoBlocksMiddle()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        do
        {
            posXBlock2 = posX[Random.Range(0, posX.Length)];
        } while (posXBlock1 == posXBlock2);
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 2);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance * 2);
        newBlock.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        spawnedBlocks.Add(newBlock);
        spawnedBlocks.Add(newBlock2);
    }

    private void SpawnThreeBlocksMiddle()
    {
        Block newBlock1 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock3 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[0];
        posXBlock2 = posX[1];
        posXBlock3 = posX[2];
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 2);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance * 2);
        Vector3 posBlock3 = new Vector3(posXBlock3, 0.5f, Player.position.z + spawnDistance * 2);
        newBlock1.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        newBlock3.transform.position = posBlock3;
        spawnedBlocks.Add(newBlock1);
        spawnedBlocks.Add(newBlock2);
        spawnedBlocks.Add(newBlock3);
    }
    private void SpawnOneBlockFar()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        Vector3 posBlock = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 3);
        newBlock.transform.position = posBlock;
        spawnedBlocks.Add(newBlock);
    }

    private void SpawnTwoBlocksFar()
    {
        Block newBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[Random.Range(0, posX.Length)];
        do
        {
            posXBlock2 = posX[Random.Range(0, posX.Length)];
        } while (posXBlock1 == posXBlock2);
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 3);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance * 3);
        newBlock.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        spawnedBlocks.Add(newBlock);
        spawnedBlocks.Add(newBlock2);
    }

    private void SpawnThreeBlocksFar()
    {
        Block newBlock1 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock2 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        Block newBlock3 = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)]);
        posXBlock1 = posX[0];
        posXBlock2 = posX[1];
        posXBlock3 = posX[2];
        Vector3 posBlock1 = new Vector3(posXBlock1, 0.5f, Player.position.z + spawnDistance * 3);
        Vector3 posBlock2 = new Vector3(posXBlock2, 0.5f, Player.position.z + spawnDistance * 3);
        Vector3 posBlock3 = new Vector3(posXBlock3, 0.5f, Player.position.z + spawnDistance * 3);
        newBlock1.transform.position = posBlock1;
        newBlock2.transform.position = posBlock2;
        newBlock3.transform.position = posBlock3;
        spawnedBlocks.Add(newBlock1);
        spawnedBlocks.Add(newBlock2);
        spawnedBlocks.Add(newBlock3);
    }
}
