using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public Door finalDoor;

    private void Update() {
        if(!finalDoor.locked && finalDoor.opened)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().speed = 0;
            RealCarlos.canMove = false;
            Debug.Log("End");
            finalDoor.opened = false;
            UIHandler.instance.FadeInImage(1f,UIHandler.instance.blackforeground);
            UIHandler.instance.DisplayFinalMessage();
            StartCoroutine(GoToCredits());
        }
    }

    IEnumerator GoToCredits()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }
}

