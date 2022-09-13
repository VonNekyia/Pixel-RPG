using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    
    private RectTransform dragTransform;
    private Canvas overlayCanvas;

    private void Awake()
    {
        dragTransform = GetComponent<RectTransform>();
        overlayCanvas = dragTransform.parent.GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragTransform.anchoredPosition += eventData.delta / overlayCanvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       dragTransform.SetAsLastSibling();
    }
}
