using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishObject : MonoBehaviour, IInteractable
{
    AudioSource audioSource;
    SkinnedMeshRenderer skin;
    bool canVanish;

    public Transform restPoint;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        canVanish = true;
    }
    public void Interact()
    {
        if(canVanish)
        {
            canVanish = false;
            audioSource.PlayOneShot(audioSource.clip);
            transform.position = restPoint.position;
            StartCoroutine(ScareCooldown());
        }
    }

    IEnumerator ScareCooldown()
    {
        yield return new WaitForSeconds(2f);
        canVanish = true;
    }

}
