using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform upcomingBlocks;
    public GameObject[] groups;

    public List<GameObject> upcomingBlockGroup = new List<GameObject>();
    private float upcomingBlockNumber = 3;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNextBlocks();
        NextBlock();
    }

    public GameObject SpawnBlock()
    {
        int x = Random.Range(0, groups.Length);
        return Instantiate(groups[x], new Vector2(0, 0), Quaternion.identity, upcomingBlocks);
    }

    public void SpawnNextBlocks()
    {
        for (int i = 0; i < upcomingBlockNumber; i++)
        {            
            GameObject newBlock = SpawnBlock();
            newBlock.transform.localPosition = new Vector2(4 * i - 4, 0);
            upcomingBlockGroup.Add(newBlock);
        }
    }

    public void NextBlock()
    {
        GameObject liveBlock = upcomingBlockGroup[0];
        liveBlock.AddComponent<Group>();
        liveBlock.transform.position = transform.position;

        upcomingBlockGroup.Remove(upcomingBlockGroup[0]);
        for (int i = 0; i < 2; i++)
        {
            upcomingBlockGroup[i].transform.position = upcomingBlockGroup[i].transform.position + new Vector3(-4, 0, 0);
        }
        GameObject newBlock = SpawnBlock();
        newBlock.transform.localPosition = new Vector2(4, 0);
        upcomingBlockGroup.Add(newBlock);
    }
}