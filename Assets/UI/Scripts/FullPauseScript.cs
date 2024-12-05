using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FullPauseScript : MonoBehaviour
{   
    public GameObject pauseMenuCanvas;
    public GameObject shopCanvas;
    private bool isPaused = false;

    public TextMeshProUGUI seedText; 

   

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        UpdateSeedText();
    }
    public void UpdateSeedText()
    {   
            seedText.text = "Seed: " + FullSettingsScript.seed.ToString();      
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuCanvas.SetActive(false);
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
    }

    public void OpenShop()
    {
        pauseMenuCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }
    public void GoToPauseMenu()
    {
        shopCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}