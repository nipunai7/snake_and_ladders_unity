using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public Text PlayerDisplay;
    public GameObject panel;

    private void Start()
    {
        panel.gameObject.SetActive(true);
        if (DBManager.LoggedIn)
        {
            PlayerDisplay.text = "Welcome " + DBManager.username + ", Select Player Modes";
        }
    }

    public void LaunchReg(int scene){
        SceneManager.LoadScene(scene);
        if (scene == 1)
        {
            SceneManager.UnloadScene(scene);
        }
    }

    public void LoadBack(int scene)
    {

        SceneManager.UnloadScene(scene);
        Time.timeScale = 1;
    }

    public void pause()
    {
        //infoBox.gameObject.SetActive(false);
        Application.LoadLevelAdditive(6);
        Time.timeScale = 0;
    }

    public void resume()
    {
        //Application.LoadLevelAdditive(7);
        //LoadBackGround.state = "paused";
        //StartCoroutine(hidePanel());
        //Debug.Log("2");
        //panel.gameObject.SetActive(false);
        SceneManager.UnloadScene(6);
        Time.timeScale = 1;
    }

    IEnumerator hidePanel()
    {       
        panel.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        Debug.Log("1");
        SceneManager.UnloadScene(6);
        Time.timeScale = 1;
        //Debug.Log("Loop Done");
    }

    public void loadGame()
    {
        LoadGame.loadgame = true;
        //GameManager.movetoLoadPosdone = true;
        SceneManager.LoadScene(3);
    }

    public void exitGame()
    {
        DialogBox.Instance.ShowQuestion("Are you sure to Exit the game?", () =>
        {
            Application.Quit();
        }, () => {

            Debug.Log("No");
        
        });
    }



}
