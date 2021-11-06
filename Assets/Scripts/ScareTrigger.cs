using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTrigger : MonoBehaviour
{
    public Transform dummy;
    public Transform spawnPosition;
    bool triggered = false;
    float threshold = 0.0f; 

    public System.Action<ScareTrigger> onScare;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !triggered)
        {
            float coin = Random.value;
            if(coin < threshold)
            {
                Vector3 dir = other.transform.forward - dummy.transform.forward;
                Debug.Log(dir);
                if(dir.x > -0.5 && dir.x < 0.5f){Debug.Log("Spawned"); onScare?.Invoke(this); dummy.position = spawnPosition.position; triggered = true;}
               
            }
        }
    }

    public void Cooldown(int seconds)
    {
        Debug.Log("Trigger Cooldown");
        StartCoroutine(CooldownSleep(seconds));
    }

    private IEnumerator CooldownSleep(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        triggered = false;
    }

    public void SetThreshold(float t)
    {
        threshold = t;
    }
}
