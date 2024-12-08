using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float scaleDuration = 0.05f;
    [SerializeField] private Color hoverColor = Color.red;

    private Vector3 originalScale;
    private TextMeshProUGUI textMeshPro;

    private void Awake(){
        textMeshPro = GetComponent<TextMeshProUGUI>();
        originalScale = textMeshPro.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData){
        SoundFXManager.PlaySoundClipForce(hoverSound, transform);
        StartCoroutine(ScaleAndChangeColor());
    }

    public void OnPointerExit(PointerEventData eventData){
        StartCoroutine(ResetScaleAndColor());
    }

    private IEnumerator ScaleAndChangeColor(){
        Vector3 targetScale = originalScale * scaleFactor;
        float timeElapsed = 0f;

        while (timeElapsed < scaleDuration){
            textMeshPro.transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / scaleDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        textMeshPro.transform.localScale = targetScale;
        textMeshPro.color = hoverColor;
    }

    private IEnumerator ResetScaleAndColor(){
        float timeElapsed = 0f;

        while (timeElapsed < scaleDuration){
            textMeshPro.transform.localScale = Vector3.Lerp(textMeshPro.transform.localScale, originalScale, timeElapsed / scaleDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        textMeshPro.transform.localScale = originalScale;
        textMeshPro.color = Color.white;
    }
}


