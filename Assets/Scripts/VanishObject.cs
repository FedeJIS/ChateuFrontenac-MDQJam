using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishObject : MonoBehaviour, IInteractable
{
    AudioSource audioSource;
    SkinnedMeshRenderer skin;
    bool canVanish;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        skin = GetComponent<SkinnedMeshRenderer>();
        canVanish = true;
    }
    public void Interact()
    {
        if(canVanish)
        {
            canVanish = false;
            skin.enabled = false;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
