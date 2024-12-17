using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandle : MonoBehaviour {
    [SerializeField] private GameObject playerInfo;
    [SerializeField] private GameObject canvas;
    void Start() => canvas.SetActive(false);
    public void Open() {
        CursorHandler.UnHide();
        Time.timeScale = 0f;
        canvas.SetActive(true);
        if (playerInfo != null) playerInfo.SetActive(false);
    }
    public void Hide() {
        CursorHandler.Hide();
        Time.timeScale = 1f;
        canvas.SetActive(false);
        if (playerInfo != null) playerInfo.SetActive(true);
    }
}