using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int alfajoresToPick = 12;
    public static int currentAlfajores = 0;

    public int keysToFind = 3;
    public GameObject alfajoresPrefab;
    public GameObject keysPrefab;
    public static bool[] roomKeys = new bool[5]; 
    public SpawnManager[] zones;
    public static System.Action onPickup;
    public static System.Action onPhoneCall;
    private static bool gameOver = false;
    private static List<Alfajor> alfajores;
    public Phone phone;

    private void Start() {
        alfajores = new List<Alfajor>();
        RayHandler.OnGrabbed += CheckGrabbed;
        //for(int i = 0; i< roomKeys.Length; i++) roomKeys[i] = true;
        SpawnItems();
        SpawnKeys();
        CheckGameState();
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
        onPickup?.Invoke();
        CheckGameState();
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
                alfajores.Add(spawned.GetComponent<Alfajor>());
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

    public void SpawnKeys()
    {
        for(int i = 0; i<3; i++) //Pongo 1 si o si en cada zona
        {
            Transform spawnPoint = zones[i].GetKeyPoint();
            if(spawnPoint != null) 
            {
                GameObject spawned = GameObject.Instantiate(keysPrefab,spawnPoint.position,spawnPoint.rotation);
                spawned.GetComponent<Key>().SetID(i+2);
                spawned.transform.SetParent(spawnPoint);
            }
        }
    }

    void CheckGameState()
    {
        switch(currentAlfajores)
        {
           
           case 0: StartCoroutine(WaitToDisplay("Encuentra 12 alfajores Balcarce")); UIHandler.instance.FadeOutImage(3f); return;
           case 2: StartCoroutine(WaitToCall());
                   return;
           case 11: roomKeys[1] = true; StartCoroutine(WaitToDisplay("Ve al sótano")); return;
           case 12: roomKeys[0] = true; StartCoroutine(WaitToDisplay("Escapa por la puerta principal")); return;
        }
    }

    private IEnumerator WaitToDisplay(string msg)
    {   
        yield return new WaitForSeconds(1.5f);
        UIHandler.instance.DisplayTitleMessage(msg);
    }

    private IEnumerator WaitToCall()
    {
        EnableAlfajores(false);
        yield return new WaitForSeconds(3f);
        phone.Ring(); 
        onPhoneCall?.Invoke();
        StartCoroutine(WaitToDisplay("Atiende el teléfono"));
    }

    public static void EnableAlfajores(bool flag)
    {
        foreach (var alfajor in alfajores)
        {
            if(alfajor != null) alfajor.transform.GetComponent<BoxCollider>().enabled = flag;        
        }
    }

    public static void EndGame()
    {
        if(!gameOver)
        {
            gameOver = true;
            UIHandler.instance.FadeInImage(1f,UIHandler.instance.blackforeground);
            UIHandler.instance.DisplayGameOverMessage();
        }
    }

    static IEnumerator GoToMainScreen()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }

    private void Update() {
        if(gameOver)
        {
            gameOver = false;
            StartCoroutine(GoToMainScreen());
        }
    }
    
}
