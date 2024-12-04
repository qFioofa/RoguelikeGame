using System.Collections;
using System.Collections.Generic;
using PSXShaderKit;
using UnityEditor.SceneManagement;
using UnityEngine;


public class PlayerEnterGameEffects : MonoBehaviour{
    [SerializeField] private PSXPostProcessEffect pSXPostProcessEffect;
    [SerializeField] private float durationEffect = 5.0f;
    [SerializeField] private float PixelValueFrom = 0.05f;
    [SerializeField] private float PixelValueTo = 7f;
    [SerializeField] private bool IsOnStart = false;
    private bool isDone; 

    void Start() {
        pSXPostProcessEffect = GetComponent<PSXPostProcessEffect>();
        if(IsOnStart){
            StartCoroutine(TogglePixelation(PixelValueFrom,PixelValueTo, durationEffect));
        }
    }

    void Update(){
        if (isDone) {
            DestroyThisScript();
            return;
        }
    }

   public IEnumerator TogglePixelation(float startValue, float endValue, float duration)
    {
        float timeElapsed = 0f;

        // Determine the direction of transition
        float fromValue = startValue;
        float toValue = endValue;

        // If we're going backwards, swap the values
        if (startValue > endValue)
        {
            fromValue = endValue;
            toValue = startValue;
        }

        // Smoother easing function (e.g., using EaseInOutQuad)
        while (timeElapsed < duration)
        {
            // Interpolation factor t: [0, 1] over time
            float t = timeElapsed / duration;

            // Smooth transition with easing (EaseInOutQuad for example)
            float easedT = EaseInOutQuad(t);

            // Lerp with the eased time factor
            float currentPixelation = Mathf.Lerp(fromValue, toValue, easedT);
            pSXPostProcessEffect.PixelationFactor = currentPixelation;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

    }

    private float EaseInOutQuad(float t)
    {
        if (t < 0.5f)
            return 2f * t * t;
        else
            return -1f + (4f - 2f * t) * t;
    }

    private void DestroyThisScript(){
        Destroy(this);
    }
}
