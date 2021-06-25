using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler
{
    public Action<PointerEventData> OnPointerClickAction;
    public Action<PointerEventData> OnPointerDragAction;
    public Action<PointerEventData> OnPointerDownAction;
    public Action<PointerEventData> OnPointerEnterAction;
    public Action<PointerEventData> OnPointerExistAction;
    public Action<PointerEventData> OnPointerUpAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickAction?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnPointerDragAction?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterAction?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExistAction?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke(eventData);
    }
}
