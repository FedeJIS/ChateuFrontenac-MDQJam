using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCarlos : MonoBehaviour
{
    public Transform player;
    public static bool canMove = true;

    Animator animator;
    AudioSource audioSource;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate() {
        
        if(canMove)
        {
            transform.LookAt(player);
            if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Run") 
            {
                if(!audioSource.isPlaying) audioSource.Play();
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.35f);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Player") && canMove)
        {
            GameManager.EndGame();
            Destroy(this.gameObject);
        }
    }


}
