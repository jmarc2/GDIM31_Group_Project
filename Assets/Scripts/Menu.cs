using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameStateManager.OnGameOver += Open;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        GameStateManager.Restart();
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
