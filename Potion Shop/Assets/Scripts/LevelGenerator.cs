using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] levelToSpawn;
    public Transform player;
    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, spawnPoint.position) < distance)
        {
            
            GameObject newestLevel = Instantiate(levelToSpawn[Random.Range(0,levelToSpawn.Length)], spawnPoint.position, Quaternion.identity);
            spawnPoint = newestLevel.GetComponent<Level>().spawnPoint;
        }
    }
}
