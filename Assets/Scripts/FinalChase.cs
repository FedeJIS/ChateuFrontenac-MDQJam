using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalChase : MonoBehaviour
{
    public GameObject carlos;
    public AudioSource audioSource;
    private void Start() {
        GameManager.onPickup += SpawnCarlos;
    }

    private void SpawnCarlos()
    {
        if(GameManager.currentAlfajores >= GameManager.alfajoresToPick) StartCoroutine(WaitForSpawn());
    }

    IEnumerator WaitForSpawn()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length+1);
        carlos.SetActive(true);

    }
}
