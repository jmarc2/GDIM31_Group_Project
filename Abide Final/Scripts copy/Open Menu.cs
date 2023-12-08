using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameStateManager.OnGameBegin += Open;

        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        GameStateManager.OnGameBegin -= Open;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
