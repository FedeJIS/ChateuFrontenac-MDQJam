using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResourceHandler : MonoBehaviour
{
   [SerializeField]
   protected float resourceLife = 100f;
   [SerializeField]
   protected float decreaseFactor = 0.5f;
   [SerializeField]
   protected float increaseFactor = 0.35f;
   [SerializeField]
   protected float penalty = 0.25f;
   
   [SerializeField]
   protected float timeToWait;

   [SerializeField]
   protected AudioClip[] sfxClips;

   [SerializeField]
   protected Sprite[] uiSprites;

   [SerializeField]
   protected Image uiIcon;

   [SerializeField]
   protected AudioSource audioSource;

   abstract public void UseResource();
   abstract public void GetResource();

   abstract public void OnEmpty();
   abstract public void OnFull();
   abstract public void OnHalf();

   abstract  protected void UpdateUI();
    

   
   
}
