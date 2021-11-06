using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareManager : MonoBehaviour
{
    int cooldown = 60;
    float threshold = 0.5f;
    const float DELTA = 0.05f;
    const int REDUCE = 5;
    public ScareTrigger[] triggers;


    private void Start() {
        foreach (ScareTrigger item in triggers)
        {
              item.onScare += OnScareTriggered;
        }

        GameManager.onPickup += UpdateValues;
    }


    private void OnScareTriggered(ScareTrigger st)
    {
            st.Cooldown(cooldown);
    }

    private void UpdateValues()
    {
        if(GameManager.currentAlfajores > 2 && GameManager.currentAlfajores < 11)
        {
            Debug.Log("Scare Started");
            threshold += DELTA;
            cooldown -= REDUCE;
            foreach (ScareTrigger item in triggers)
            {
                item.SetThreshold(threshold);                
            }
        }
        else if(GameManager.currentAlfajores >= 11)
        {
            foreach (ScareTrigger item in triggers)
            {
                if(item)
                Destroy(item.gameObject);               
            }
        }
        else
        {
            Debug.Log("Scares dont started yet");
        }
    }
}
