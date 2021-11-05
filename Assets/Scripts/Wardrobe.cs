using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public float toMove;
    public float origin;
    public bool opened = false;

    private void Start() {
        origin = transform.localPosition.z;
    }
    public void Interact()
    {
        OpenClose();
    }

    public void OpenClose()
    {
        if(!opened){ audioSource.pitch = 1.25f; LeanTween.moveLocalZ(this.gameObject,toMove,0.15f);}
        else {audioSource.pitch = 1f; LeanTween.moveLocalZ(this.gameObject,origin,0.15f);}
        opened = !opened;
        audioSource.Play();
    }
}
