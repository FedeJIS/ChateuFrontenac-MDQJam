using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int alfajoresToPick = 12;
    public static int currentAlfajores = 0;

    public GameObject alfajoresPrefab;
    public static bool[] roomKeys = new bool[4]; 

    public SpawnManager[] zones;

    private void Start() {
        RayHandler.OnGrabbed += CheckGrabbed;
        for(int i = 0; i< roomKeys.Length; i++) roomKeys[i] = true;
        SpawnItems();
    }

    void CheckGrabbed(GameObject grabbed)
    {
        if(grabbed.GetComponent<Alfajor>() != null)
        {
            AddAlfajor();
        }
    }
    void AddAlfajor()
    {
        currentAlfajores++;
        Debug.Log("Current Alfajores: "+currentAlfajores);
    }

    void CheckGameState()
    {

    }

    public void SpawnItems()
    {
        int alfajoresToSpawn = alfajoresToPick-1;
        foreach (var zone in zones) //Pongo 1 si o si en cada zona
        {
            Transform spawnPoint = zone.GetSpawnPoint();
            if(spawnPoint != null) 
            {
                GameObject spawned = GameObject.Instantiate(alfajoresPrefab,spawnPoint.position,spawnPoint.rotation);
                spawned.transform.SetParent(spawnPoint);
                alfajoresToSpawn--;
            }
        }

        while(alfajoresToSpawn > 0)
        {
            int randomIndex = Random.Range(0,zones.Length);
            Transform spawnPoint = zones[randomIndex].GetSpawnPoint();   
            if(spawnPoint != null) 
            {
                GameObject spawned = GameObject.Instantiate(alfajoresPrefab,spawnPoint.position,spawnPoint.rotation);
                spawned.transform.SetParent(spawnPoint);
                alfajoresToSpawn--;
            }
        }
    }
}
