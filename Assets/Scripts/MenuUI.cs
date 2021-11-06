using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public void OnPointerEnter(PointerEventData eventData)
      {
          Debug.Log("Enter");
         GetComponent<Image>().enabled = true;
      }
      public void OnSelect(BaseEventData eventData)
      {
          Debug.Log("Exit");
          GetComponent<Image>().enabled = false;
      }
}
