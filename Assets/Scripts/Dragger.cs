using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dragger : MonoBehaviour, IDragHandler
{
    RectTransform rectTransform;
    [SerializeField] RectTransform target;
    [SerializeField] Canvas canvas;
    [SerializeField] UnityEvent OnPlaced;
    Image image;
    bool placed = false;
    
    Vector3 startingPosition;
    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placed) { return; }
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    private void Update()
    {
        if (RectOverlaps(rectTransform, target))
        {
            OnPlaced?.Invoke();
            image.enabled = false;
            placed = true;
        }
    }

    public void ResetPosition()
    {
        rectTransform.position = startingPosition;
        image.enabled = true;
        placed = false;
    }

    private bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}
