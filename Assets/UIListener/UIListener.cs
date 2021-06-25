using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler
{
    public Action<PointerEventData, object[]> OnPointerClickAction;
    public Action<PointerEventData, object[]> OnPointerDragAction;
    public Action<PointerEventData, object[]> OnPointerDownAction;
    public Action<PointerEventData, object[]> OnPointerEnterAction;
    public Action<PointerEventData, object[]> OnPointerExistAction;
    public Action<PointerEventData, object[]> OnPointerUpAction;

    public object[] Args = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickAction?.Invoke(eventData, Args);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnPointerDragAction?.Invoke(eventData, Args);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke(eventData, Args);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterAction?.Invoke(eventData, Args);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExistAction?.Invoke(eventData, Args);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke(eventData, Args);
    }
}
