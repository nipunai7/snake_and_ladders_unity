using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBackGround : MonoBehaviour
{
    public static string state;
    public void panelvisibility()
    {
        Debug.Log("Going to Loop");
        Renderer panel = gameObject.GetComponent<Renderer>();
        panel.enabled = false;
        StartCoroutine(hidePanel(3));

    }

    IEnumerator hidePanel(int scene)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scene);
        Debug.Log("Loop Done");
    }

    /* private void Start()
     {
         if (login.instance.state == "paused")
         {
             SceneManager.UnloadScene(7);
             Time.timeScale = 1;           
         }
         else
         {
             SceneManager.LoadScene(3);
             SceneManager.UnloadScene(7);
         }
     }*/

    private void Start()
    {
        if (state == "paused")
        {
            Time.timeScale = 1;
            SceneManager.UnloadScene(7);
            Debug.Log("7 resumed");
        } else
        {
            Debug.Log("7 loaded");
            SceneManager.LoadScene(3);
            SceneManager.UnloadScene(7);
        }
    }
}
