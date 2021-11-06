using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTrigger : MonoBehaviour
{
    public Transform dummy;
    public Transform spawnPosition;
    bool triggered = false;
    float threshold = 0.15f; 
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !triggered)
        {
            float coin = Random.value;
            if(coin < threshold)
            {
                Debug.Log("Spawned");
                Vector3 dir = other.transform.forward - dummy.transform.forward;
                if(dir.x > 0 && dir.x < 0.3f) dummy.position = spawnPosition.position;
                triggered = true;
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Listo");
        triggered = false;
    }
}
