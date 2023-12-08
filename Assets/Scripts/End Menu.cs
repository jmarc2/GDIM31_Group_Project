using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{

    [SerializeField] private AudioSource die;

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
        die.Play();
        gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public static void QuitEnd()
    {
        Application.Quit();
    }
}

