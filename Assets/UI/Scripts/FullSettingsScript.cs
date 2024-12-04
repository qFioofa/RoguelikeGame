using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class FullSettingsScript : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public TMP_InputField inputField; 
    public string seed;
    void Start() {
        settingsCanvas.SetActive(false);
        inputField.onValueChanged.AddListener(ValidateInput);
    }

    void OnDestroy() {
        inputField.onValueChanged.RemoveListener(ValidateInput);
    }
    private void ValidateInput(string input)
    {
        string validated = "";
        foreach (char c in input) {

            if (char.IsDigit(c)) 
                validated += c;
        }

        if (validated != input)
            inputField.text = validated;
        seed = inputField.text;
    }
    public void ShowSettingsCanvas()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void ShowMainMenuCanvas()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    public void SetRandomSeed()
    {
        System.Random random = new System.Random(); 
        int randomNumber = random.Next(0, int.MaxValue);
        inputField.text = randomNumber.ToString();
    }
}
