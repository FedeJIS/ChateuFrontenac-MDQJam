using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler: MonoBehaviour
{
   public static UIHandler instance;
   public  TextMeshProUGUI interactableMessage;
   public  TextMeshProUGUI conditionMessage;
   public  TextMeshProUGUI messageTitle;
   public TextMeshProUGUI finalTitle;

   public TextMeshProUGUI gameOverTitle;
   public System.Action onFadeEnded;
   public Image blackforeground;
    private void Awake() {
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
        StartCoroutine(FadeTextToFullAlpha(0.45f,messageTitle));
        StartCoroutine(FadeTextToZeroAlpha(0.45f,messageTitle));
   }

   public void DisplayFinalMessage()
   {
        finalTitle.gameObject.SetActive(true);
        TextMeshProUGUI body = finalTitle.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeTextToFullAlpha(1.5f,finalTitle));
        StartCoroutine(FadeTextToFullAlpha(1.5f,body));
        StartCoroutine(FadeTextToZeroAlpha(6f,finalTitle)); 
        StartCoroutine(FadeTextToZeroAlpha(6f,body)); 
   }

   public void DisplayGameOverMessage()
   {
        gameOverTitle.gameObject.SetActive(true);
        TextMeshProUGUI body =  gameOverTitle.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeTextToFullAlpha(1.5f, gameOverTitle));
        StartCoroutine(FadeTextToFullAlpha(1.5f,body));
        StartCoroutine(FadeTextToZeroAlpha(6f, gameOverTitle)); 
        StartCoroutine(FadeTextToZeroAlpha(6f,body)); 
   }

   public void DisplayPresentation()
   {
        gameOverTitle.gameObject.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(1.5f, gameOverTitle));
   }

   public void FadeOutImage(float t, Image i = null){ 
       if(i == null) StartCoroutine(FadeImageToZeroAlpha(t,blackforeground)); 
       else StartCoroutine(FadeImageToZeroAlpha(t,i));}
   public void FadeInImage(float t, Image i){ StartCoroutine(FadeImageToFullAlpha(t,i));}

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

     public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.gameObject.SetActive(true);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
        yield return new WaitForSeconds(1.5f);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        onFadeEnded?.Invoke();
        i.gameObject.SetActive(false);
    }

}
