using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FullSettingsScript : MonoBehaviour {
    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public TMP_InputField inputField;
    public Slider volumeSlider;
    public Slider sensitivityXSlider;
    public Slider sensitivityYSlider;

    static public string seed = "12";
    public static float sensitivityX = 1f;
    public static float sensitivityY = 1f;

    public int sensitivityMultuplayer = 40;

    void Start()
    {
        settingsCanvas.SetActive(false);
        inputField.onValueChanged.AddListener(ValidateInput);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivityXSlider.onValueChanged.AddListener(SetSensitivityX);
        sensitivityYSlider.onValueChanged.AddListener(SetSensitivityY);

        LoadSettings();
    }

    void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(ValidateInput);
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
        sensitivityXSlider.onValueChanged.RemoveListener(SetSensitivityX);
        sensitivityYSlider.onValueChanged.RemoveListener(SetSensitivityY);
    }

    private void ValidateInput(string input)
    {
        string validated = "";
        foreach (char c in input)
        {
            if (char.IsDigit(c)) validated += c;
        }

        if (validated != input) inputField.text = validated;
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
        SaveSettings();
    }

    public void SetRandomSeed()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, int.MaxValue);
        inputField.text = randomNumber.ToString();
    }

    public void CopySeedToClipboard(){
        if (!string.IsNullOrEmpty(inputField.text)) GUIUtility.systemCopyBuffer = inputField.text;
    }

    private void SaveSettings() {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("SensitivityX", sensitivityXSlider.value);
        PlayerPrefs.SetFloat("SensitivityY", sensitivityYSlider.value);
        PlayerPrefs.SetString("Seed", seed);
        PlayerPrefs.Save(); 
    }

    private void LoadSettings() {
        if (PlayerPrefs.HasKey("Volume")) volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        if (PlayerPrefs.HasKey("SensitivityX")) sensitivityXSlider.value = PlayerPrefs.GetFloat("SensitivityX");

        if (PlayerPrefs.HasKey("SensitivityY")) sensitivityYSlider.value = PlayerPrefs.GetFloat("SensitivityY");

        if (PlayerPrefs.HasKey("Seed")) {
            seed = PlayerPrefs.GetString("Seed");
            inputField.text = seed;
        }
    }

    private void SetVolume(float value) => AudioListener.volume = value;
    private void SetSensitivityX(float value) => sensitivityX = value*sensitivityMultuplayer;
    private void SetSensitivityY(float value) => sensitivityY = value*sensitivityMultuplayer;
}
