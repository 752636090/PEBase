using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowRoot : MonoBehaviour
{
    protected void RegisterClick(GameObject go, Action<PointerEventData, object[]> onClick, params object[] args)
    {
        UIListener listener = go.GetComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnPointerClickAction = onClick;
        if (args != null)
        {
            listener.Args = args;
        }
    }
}
