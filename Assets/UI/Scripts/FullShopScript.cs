using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullShopScript : MonoBehaviour
{
    
    public GameObject pauseMenuCanvas;
    public GameObject shopCanvas;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shopCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}