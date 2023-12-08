using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P)) 
        {
            PauseGame();
        }
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void GoToMainMenu() 
    {
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame() 
    {
        isPaused = false;
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

}
