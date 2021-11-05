using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler: MonoBehaviour
{
    public static UIHandler instance;
   public  TextMeshProUGUI interactableMessage;
   public  TextMeshProUGUI conditionMessage;
   public  TextMeshProUGUI messageTitle;

    private void Start() {
        instance = this;
    }

   public void DisplayInteractionMessage(string text = null)
   {
        if(text != null) interactableMessage.text = text;
        else interactableMessage.text = "'E' para interactuar";
        interactableMessage.gameObject.SetActive(true);
   }

   public void DissapearInteractionMessage()
   {
       interactableMessage.gameObject.SetActive(false);
   }

   public void DisplayConditionMessage(string text)
   {
       
       conditionMessage.text = text;
       conditionMessage.gameObject.SetActive(true);
       StartCoroutine(FadeTextToFullAlpha(0.25f,conditionMessage));
       StartCoroutine(FadeTextToZeroAlpha(0.25f,conditionMessage));
   }

   public void DisplayTitleMessage(string text)
   {
        messageTitle.text = text;
        messageTitle.gameObject.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(0.35f,messageTitle));
        StartCoroutine(FadeTextToZeroAlpha(0.35f,messageTitle));
   }

   public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        
        
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        yield return new WaitForSeconds(1.5f);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        i.gameObject.SetActive(false);
    }

}
