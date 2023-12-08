using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class GameStateManager : MonoBehaviour
{
    public static Action OnGameOver;
    public static Action OnGameBegin;
    private static GameStateManager _instance;

    [SerializeField] Canvas gameEnd;
  
    // Start is called before the first frame update
    void Start()
    { 
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }

        else
            if (_instance != this)
        {
            Destroy(gameObject);
        }

        Begin();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Begin() 
    {
        OnGameBegin.Invoke();
        Time.timeScale = 0;
    }

    public static void GameOver()
    {
        OnGameOver.Invoke();
        Time.timeScale = 0;
    }

    public static void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
