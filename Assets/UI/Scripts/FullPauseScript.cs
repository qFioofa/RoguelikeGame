using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FullPauseScript : MonoBehaviour {
    [Header("Canvases")]
    public GameObject pauseMenuCanvas;
    public GameObject InfoCanvas;
    public GameObject DefaultCanvas;
    private bool isPaused = false;

    [Header("UI")]
    public TextMeshProUGUI seedText;
    public string tutorialNameScene = "Poligon";

    void Start() {
        pauseMenuCanvas.SetActive(false);
        UpdateSeedText();
    }

    public void UpdateSeedText() {
        if(SceneManager.GetActiveScene().name ==tutorialNameScene) seedText.text = "Seed: Tutorial";
        else seedText.text = "Seed: " + FullSettingsScript.seed.ToString();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) ResumeGame();
            else TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) EnterPause();
        else ResumeGame();
    }

    public void EnterPause() {
        CursorHandler.UnHide();
        Time.timeScale = 0f;
        if(InfoCanvas!= null) InfoCanvas.SetActive(false);
        if(DefaultCanvas!= null) DefaultCanvas.SetActive(false);
        if(pauseMenuCanvas!= null) pauseMenuCanvas.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame() {
        CursorHandler.Hide();
        Time.timeScale = 1f;
        if(InfoCanvas!= null) InfoCanvas.SetActive(true);
        if(DefaultCanvas!= null) DefaultCanvas.SetActive(true);
        if(pauseMenuCanvas!= null) pauseMenuCanvas.SetActive(false);
        isPaused = false;
    }

    public void GoToPauseMenu() => pauseMenuCanvas.SetActive(true);

    public void GoToMainMenu() {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu");
    }
}
