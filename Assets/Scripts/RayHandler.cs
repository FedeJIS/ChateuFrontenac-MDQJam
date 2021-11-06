 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHandler : MonoBehaviour
{
    Ray ray;
    RaycastHit hitData;
    Ray longDistanceRay;
    RaycastHit longData;
    public static System.Action<GameObject> OnGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward*0.25f);
        Debug.DrawRay(ray.origin, ray.direction * 10,Color.green);

        longDistanceRay = new Ray(transform.position,transform.forward);
        //Debug.DrawRay(longDistanceRay.origin, longDistanceRay.direction * 100,Color.red);
        if (Physics.Raycast(ray, out hitData))
        {
            var interactable = hitData.transform.gameObject.GetComponent<IInteractable>();
            if(interactable != null) 
            {
                if(hitData.distance < 10)
                {
                    UIHandler.instance.DisplayInteractionMessage();
                    if(Input.GetKeyDown(KeyCode.E))
                    {   
                        OnGrabbed?.Invoke(hitData.transform.gameObject);
                        interactable.Interact();
                    }
                }
                else
                {
                    hitData = new RaycastHit();
                    UIHandler.instance.DissapearInteractionMessage();
                   
                }
            }else{ hitData = new RaycastHit(); UIHandler.instance.DissapearInteractionMessage();}
        }

        if(Physics.Raycast(longDistanceRay, out longData))
        {
            if(longData.distance < 100)
            {
                if(BatteryHandler.isOn && longData.transform.gameObject.CompareTag("Dummy")) 
                {
                    var vanish = hitData.transform.gameObject.GetComponent<VanishObject>();
                    vanish.Interact();
                    longData = new RaycastHit();
                }
            }
        }
    }

}
