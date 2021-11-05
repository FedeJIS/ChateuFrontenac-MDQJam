using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHandler : ResourceHandler
{

    [SerializeField]
    private Light spotlight;

    private float batteryLife;
    private bool canUse = true;
    public static bool isOn = true;
    private bool canPenalty = false;
    public static Action<bool> onSwitch;

    private void Start() {
        batteryLife = resourceLife;
        audioSource.clip = sfxClips[0];
        SwitchFlashlight(true);
    }
    private void Update() {
        if(batteryLife <= 0) SwitchFlashlight(false);
    }
    public override void UseResource()
    {
        if(isOn) StartCoroutine(UseBattery());
    }
    public override void GetResource()
    {
        if(!isOn) StartCoroutine(RecoverBattery());
        
    }

    public void SwitchFlashlight(bool turn)
    {
        if(canUse)
        {
            canUse = false;
            isOn = turn;
            Debug.Log(isOn);
            spotlight.gameObject.SetActive(isOn);
            onSwitch?.Invoke(isOn);
            if(isOn){
                    audioSource.pitch = 1.25f;
                    UseResource();
                }
            else {
                    audioSource.pitch = 1f;
                    GetResource();
                }
            audioSource.Play();
            StartCoroutine(CanUse());  
        }
        else
        {
            if(spotlight.gameObject.activeSelf && canPenalty){ Debug.Log("PENALTY"); canPenalty = false; batteryLife -= penalty;}
        }
            
             
          
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

    private IEnumerator UseBattery()
    {
        while(batteryLife > 0)
        {
            yield return new WaitForSeconds(timeToWait);
            batteryLife -= decreaseFactor;
            if(batteryLife <= 0) batteryLife = 0;
            UpdateUI();
            if(!isOn) break;

        }
        
    }

    private IEnumerator RecoverBattery()
    {
        while(batteryLife  <= resourceLife)
        {
            yield return new WaitForSeconds(timeToWait);
            batteryLife += increaseFactor;
            if(batteryLife >= resourceLife) batteryLife = resourceLife;
            UpdateUI();
            if(isOn) break;
            
        }
    } 

    private IEnumerator CanUse()
    {
        yield return new WaitForSeconds(timeToWait*0.5f);
        canUse = true;
        canPenalty = true;
    }

    protected override void UpdateUI()
    {
        if(batteryLife >= resourceLife*0.9f && batteryLife  < resourceLife)
            uiIcon.sprite = uiSprites[5];
        else if(batteryLife >= resourceLife*0.6f && batteryLife  < resourceLife*0.8f)
            uiIcon.sprite = uiSprites[4];
        else if(batteryLife >= resourceLife*0.5f && batteryLife  < resourceLife*0.6f)
            uiIcon.sprite = uiSprites[3];
        else if(batteryLife >= resourceLife*0.2f && batteryLife  < resourceLife*0.4f)
            uiIcon.sprite = uiSprites[2];
        else if(batteryLife > resourceLife*0f && batteryLife  < resourceLife*0.2f)
            uiIcon.sprite = uiSprites[1];
        else if(batteryLife <= 0)
            uiIcon.sprite = uiSprites[0];
    }
}
