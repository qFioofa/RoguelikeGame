using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public abstract class ButtonClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    protected TextMeshProUGUI textMeshPro;
    private bool isPointerOverButton = false;

    protected virtual void Awake(){
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    protected abstract void HandleRightClick();
    public virtual void OnPointerEnter(PointerEventData eventData){
        isPointerOverButton = true;
    }
    public virtual void OnPointerExit(PointerEventData eventData){
        isPointerOverButton = false;
    }

    protected virtual void Update(){
        if (isPointerOverButton && Input.GetMouseButtonDown(0)){
            HandleRightClick();
        }
    }
}