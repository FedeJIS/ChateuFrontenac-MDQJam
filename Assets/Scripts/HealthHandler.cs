using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HealthHandler : ResourceHandler
{
  
    private float mentalLife;
    private bool afraid;
    public GameObject breath;

    public GameObject globalVolume;
     ColorCurves color;
     ChromaticAberration chroma;

     bool allucinating = false;
    // Start is called before the first frame update
    void Start()
    {
       BatteryHandler.onSwitch += SetAfraid;
       mentalLife = resourceLife;
       SetAfraid(false);
       SetAfraid(true);
      
       globalVolume.GetComponent<Volume>().profile.TryGet<ColorCurves>(out color); 
       globalVolume.GetComponent<Volume>().profile.TryGet<ChromaticAberration>(out chroma);
    }


    public override void GetResource()
    {
        StartCoroutine(RecoverHealth());
    }

     public override void UseResource()
    {
        StartCoroutine(LoseHealth());
    }

    public override void OnEmpty()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFull()
    {
        throw new System.NotImplementedException();
    }

    public override void OnHalf()
    {
        throw new System.NotImplementedException();
    }
    protected override void UpdateUI()
    {
        if(mentalLife >= resourceLife*0.9f && mentalLife  <= resourceLife)
            uiIcon.sprite = uiSprites[10];
        else if(mentalLife >= resourceLife*0.8f && mentalLife  < resourceLife*0.9f)
            uiIcon.sprite = uiSprites[9];
        else if(mentalLife >= resourceLife*0.7f && mentalLife  < resourceLife*0.8f)
            uiIcon.sprite = uiSprites[8];
        else if(mentalLife >= resourceLife*0.6f && mentalLife  < resourceLife*0.7f)
            uiIcon.sprite = uiSprites[7];
        else if(mentalLife >= resourceLife*0.5f && mentalLife  < resourceLife*0.6f)
            uiIcon.sprite = uiSprites[6];
        else if(mentalLife >= resourceLife*0.4f && mentalLife  < resourceLife*0.5f)
            uiIcon.sprite = uiSprites[5];
        else if(mentalLife >= resourceLife*0.3f && mentalLife  < resourceLife*0.4f)
        uiIcon.sprite = uiSprites[4];
        else if(mentalLife >= resourceLife*0.2f && mentalLife  < resourceLife*0.3f)
        uiIcon.sprite = uiSprites[3];
        else if(mentalLife > resourceLife*0.1f && mentalLife  < resourceLife*0.2f)
            uiIcon.sprite = uiSprites[2];
        else if(mentalLife > resourceLife*0f && mentalLife  < resourceLife*0.1f)
            uiIcon.sprite = uiSprites[1];    
        else if(mentalLife <= 0)
            uiIcon.sprite = uiSprites[0];
    }
    public void SetAfraid(bool flag){
        afraid = !flag;
        if(afraid) UseResource();
        else GetResource();
        }
    private IEnumerator LoseHealth()
    {
        while(mentalLife > 0 && afraid)
        {
            yield return new WaitForSeconds(timeToWait);
            mentalLife -= decreaseFactor;
            if(mentalLife <= 0) mentalLife = 0;
            UpdateUI();
            CheckHealth();
        
        }
        
    }

    private IEnumerator RecoverHealth()
    {
        while(mentalLife  <= resourceLife && !afraid)
        {
            yield return new WaitForSeconds(timeToWait);
            mentalLife += increaseFactor;
            if(mentalLife >= resourceLife) mentalLife = resourceLife;
            UpdateUI();
            CheckHealth();
            
        }
    } 

    private void CheckHealth()
    {
       

        if(mentalLife > resourceLife) audioSource.clip = sfxClips[0];
        else if(mentalLife >= resourceLife*0.25f && mentalLife <= resourceLife*0.5f)
        {
            audioSource.clip = sfxClips[1]; breath.gameObject.SetActive(false);
            AfraidEffects(false);
            allucinating = false;
        }
        else if(mentalLife >= 0 && mentalLife < resourceLife*0.25f)
        {
            audioSource.clip = sfxClips[2]; breath.gameObject.SetActive(true);
            AfraidEffects(true);
        }
        if(!audioSource.isPlaying) audioSource.Play();
    }

    private void AfraidEffects(bool flag)
    {
        ColorCurves color;
        globalVolume.GetComponent<Volume>().profile.TryGet<ColorCurves>(out color);
        ChromaticAberration chroma;
        globalVolume.GetComponent<Volume>().profile.TryGet<ChromaticAberration>(out chroma);
        if(color) color.active = flag;
        if(chroma) chroma.active = flag;
        if(flag && !allucinating) StartCoroutine(FadeInAllucination());
    }

    private IEnumerator FadeInAllucination()
    {   
        allucinating = true;
        while(allucinating)
        {
            float elapsedTime = 0;
    
            while (elapsedTime < sfxClips[2].length/2)
            {
                elapsedTime += Time.deltaTime;
                chroma.intensity.value = Mathf.Lerp(1f, 0f, elapsedTime/(sfxClips[2].length/2));
                CameraShake.Shake((sfxClips[2].length/2f), 0.015f);
                yield return null;
            }
            if(!allucinating) break;
         }
         yield return null;
    }
    



}
