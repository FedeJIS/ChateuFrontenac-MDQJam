using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void StartGame()
    {
        UIHandler.instance.FadeInImage(1,UIHandler.instance.blackforeground);
        UIHandler.instance.DisplayPresentation();
        StartCoroutine(WaitToLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadSceneAsync(1);
    }
}
