using UnityEngine;
using UnityEngine.UI;

public class ScrollLimit1 : MonoBehaviour
{

    public ScrollRect scrollRect;
    public RectTransform content;

    void Update()
    {
        Vector2 scrollPosition = scrollRect.normalizedPosition;
        if (scrollPosition.y > 1.0f)
        {
            scrollPosition.y = 1.0f;
        }
        if (scrollPosition.y <= 0.0f)
        {
            scrollPosition.y = 0.0f;
        }
        scrollRect.normalizedPosition = scrollPosition;
    }


}