using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReligiousTrigger : MonoBehaviour
{
    AudioSource audioSource;
    bool triggered = false;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(!triggered)
        {
            Debug.Log("Triggered");
            if(other.CompareTag("Player"))
            {   triggered = true;
                audioSource.Play();
            }
        }
    }
}
