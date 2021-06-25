using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IInitializePotentialDragHandler
{
    public Action<PointerEventData, GameObject, object[]> OnPointerClickAction;
    public Action<PointerEventData, GameObject, object[]> OnDragAction;
    public Action<PointerEventData, GameObject, object[]> OnPointerDownAction;
    public Action<PointerEventData, GameObject, object[]> OnPointerEnterAction;
    public Action<PointerEventData, GameObject, object[]> OnPointerExistAction;
    public Action<PointerEventData, GameObject, object[]> OnPointerUpAction;
    public Action<PointerEventData, GameObject, object[]> OnBeginDragAction;
    public Action<PointerEventData, GameObject, object[]> OnEndDragAction;
    public Action<PointerEventData, GameObject, object[]> OnDropAction;
    public Action<PointerEventData, GameObject, object[]> OnScrollAction;
    public Action<PointerEventData, GameObject, object[]> OnInitializePotentialDragAction;

    public object[] Args = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExistAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnScroll(PointerEventData eventData)
    {
        OnScrollAction?.Invoke(eventData, gameObject, Args);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        OnInitializePotentialDragAction?.Invoke(eventData, gameObject, Args);
    }
}
