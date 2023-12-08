using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameStateManager.OnGameOver += OpenEnd;

        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        GameStateManager.OnGameOver -= OpenEnd;
    }

    public void OpenEnd()
    {
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    public static void QuitEnd()
    {
        Application.Quit();
    }
}

