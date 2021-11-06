using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public int id;
    public string roomName;
    public void Interact()
    {
        UIHandler.instance.DisplayTitleMessage("Has encontrado la llave de "+roomName);
        GameManager.roomKeys[id] = true;
        Destroy(this.gameObject);

    }

    public void SetID(int id)
    {
        this.id = id;
        switch(id)
        {
            case 2: roomName = "Patio"; return;
            case 3: roomName = "Habitación 3"; return;
            case 4: roomName = "Habitacion 666"; return;
        }
    }


}
