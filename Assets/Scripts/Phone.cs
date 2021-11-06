using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour,IInteractable
{
    public BoxCollider myCollider;
    public AudioClip[] clips;
    public AudioSource audioSource;
    public GameObject rayHandler;
    public void Ring()
    {
        myCollider.enabled = true;
        audioSource.clip = clips[0];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void Interact()
    {
      myCollider.enabled = false;
      audioSource.clip = clips[1];
      audioSource.loop = false;
      audioSource.Play();
      StartCoroutine(WaitToHold());
      
    }

    private IEnumerator WaitToHold()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        GameManager.EnableAlfajores(true);
    }
}
