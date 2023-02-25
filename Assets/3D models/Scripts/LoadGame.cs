using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public static bool loadgame = false;
    public static int[] stepsdoneDB = new int[3];

    // Start is called before the first frame update

    public void Start()
    {
        if (loadgame)
        {
            Debug.Log("function loaded");
            StartCoroutine(getData());
            loadgame = false;
        }
    }

    IEnumerator getData()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", DBManager.username);
        WWW www = new WWW("https://agrohub.ml/sqlconnect/webtest.php", form);
        yield return www;

        if (www.text[0] == '0')
        {
            Debug.Log("Positions Loaded");

            stepsdoneDB[0] = int.Parse(www.text.Split('\t')[1]);
            //stepsdoneDB[1] = int.Parse(www.text.Split('\t')[2]);
            stepsdoneDB[1] = int.Parse(www.text.Split('\t')[3]);
            stepsdoneDB[2] = int.Parse(www.text.Split('\t')[4]);
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            

            Debug.Log("Red: " + stepsdoneDB[0] + "\t" + "Green: " + stepsdoneDB[1] + "\t" + "Yellow: " + stepsdoneDB[2] + "\t");
            GameManager.movetoLoadPosdone = true;
        }
        else
        {
            Debug.Log("Loggin error: " + www.text);
        }
    }
}
