using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool locked;
    public string message;
    public string conditionMessage;
    public AudioClip[] clips;
    public AudioSource audioSource;
    public bool opened = false;
    public int id = -1;

    public Vector2 fromto;
    public void Interact()
    {
        OpenClose();
    }

    private void OpenClose()
    {
        if(!locked)
        {
            if(!opened){ audioSource.clip = clips[0]; LeanTween.rotateLocal(this.gameObject,new Vector3(0,fromto.y,0),0.35f);}
            else {audioSource.clip = clips[1]; LeanTween.rotateLocal(this.gameObject,new Vector3(0,fromto.x,0),0.35f);}
            opened = !opened;
            audioSource.pitch = 2;
            audioSource.Play();
        }
        else
        {
            if(id >= 0 && GameManager.roomKeys[id]){ 
                if(clips.Length > 2){ audioSource.clip = clips[2]; audioSource.Play();}
                locked = false; conditionMessage = "Puerta desbloqueada"; 
                UIHandler.instance.DisplayConditionMessage(conditionMessage);
                
                }
            else{
                    if(conditionMessage == "") UIHandler.instance.DisplayConditionMessage("Debes juntar "+(GameManager.alfajoresToPick - GameManager.currentAlfajores)+" alfajores más antes de salir");
                    else UIHandler.instance.DisplayConditionMessage(conditionMessage);
            }
        }
    }
}
