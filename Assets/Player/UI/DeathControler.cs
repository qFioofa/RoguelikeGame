using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathControler : MonoBehaviour {
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI statText; 
    [SerializeField] private GameObject statsCanvas;

    [Header("Hide canvases")]
    [SerializeField] private GameObject playerPauseCanvas;
    [SerializeField] private GameObject playerInfoCanvas;
    [SerializeField] private GameObject playerCrosshairCanvas;

    private PlayerLook playerLook;

    private StatisticCollecter statisticCollecter;

    void Start(){
        statsCanvas.SetActive(false);
        statisticCollecter = GameObject.FindWithTag("Statistics")?.GetComponent<StatisticCollecter>();
        playerLook = GameObject.FindWithTag("Player").GetComponent<PlayerLook>();
    }

    public void ShowStats(){
        CursorHandler.UnHide();
        hideUI();
        playerLook.CanLook = false;
        statText.text = statisticCollecter.getStatistic();
        statsCanvas.SetActive(true);
    }

    private void hideUI(){
        Destroy(playerPauseCanvas);
        Destroy(playerInfoCanvas);
        Destroy(playerCrosshairCanvas);
    }

    public void backMenu(){
        SceneManager.LoadScene("Menu");
    }
}
