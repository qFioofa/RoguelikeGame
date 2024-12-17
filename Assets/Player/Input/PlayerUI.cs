using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public void UpdateText(string promptMessage){
        textMeshProUGUI.text = promptMessage;
    }

}