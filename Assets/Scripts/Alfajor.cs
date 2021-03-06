using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alfajor : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private BoxCollider myCollider;

    private void Start() {
         audioSource = GetComponent<AudioSource>();
         myCollider = GetComponent<BoxCollider>();
    }
    public void Interact()
    {
        UIHandler.instance.DisplayTitleMessage("Alfajor "+GameManager.currentAlfajores+"/"+GameManager.alfajoresToPick);
        audioSource.pitch = Random.Range(1,1.2f);
        audioSource.Play();
        Destroy(this.gameObject,audioSource.clip.length);
    }

}
