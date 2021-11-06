using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public SpawnPoint[] keyPoints;
    public int maxSpawns;

    private void Start() {
    }
    public Transform GetSpawnPoint()
    {
        if(spawnPoints.Length > 0 && maxSpawns > 0)
        {
            maxSpawns -= 1;
            int randomIndex = Random.Range(0,spawnPoints.Length);
            while(spawnPoints[randomIndex].IsUsed())
            {
                randomIndex = Random.Range(0,spawnPoints.Length);
            }
            spawnPoints[randomIndex].SetUsed();
            return spawnPoints[randomIndex].GetSpawnpoint();
        }
        return null;
    }

    public Transform GetKeyPoint()
    {
        if(keyPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, keyPoints.Length);
            while(keyPoints[randomIndex].IsUsed())
            {
                randomIndex = Random.Range(0,keyPoints.Length);
            }
            keyPoints[randomIndex].SetUsed();
            return keyPoints[randomIndex].GetSpawnpoint();
        }
        return null;
    }
}

// if(!spawnPoints[randomIndex].IsUsed())
//             {
//                 maxSpawns -= 1;
//                 spawnPoints[randomIndex].SetUsed();
//                 return spawnPoints[randomIndex].GetSpawnpoint();
//             }