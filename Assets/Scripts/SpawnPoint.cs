using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    bool used = false;
    public bool IsUsed(){return used;}
    public void SetUsed(){used = true;}
    public Transform GetSpawnpoint(){ return transform;}
}
