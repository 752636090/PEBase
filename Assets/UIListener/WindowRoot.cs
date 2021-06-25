using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NS_WindowRootUtil;

namespace NS_WindowRootUtil
{
    static class Extentions
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T t = go.GetComponent<T>();
            if (t == null)
            {
                t = go.AddComponent<T>();
            }
            return t;
        }
    }
}

public class WindowRoot : MonoBehaviour
{
    protected void RegisterPointerClick(GameObject go, Action<PointerEventData, GameObject, object[]> onClick, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        listener.OnPointerClickAction = onClick;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    protected void RegisterPointerDown(GameObject go, Action<PointerEventData, GameObject, object[]> onClickDown, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnPointerDownAction = onClickDown;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    protected void RegisterPointerUp(GameObject go, Action<PointerEventData, GameObject, object[]> onClickUp, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnPointerUpAction = onClickUp;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    protected void RegisterDrag(GameObject go, Action<PointerEventData, GameObject, object[]> onDrag, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnDragAction = onDrag;
        if (args != null)
        {
            listener.Args = args;
        }
    }
}

public static class Extentions
{
    public static void RegisterPointerClick(this GameObject go, Action<PointerEventData, GameObject, object[]> onClick, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        listener.OnPointerClickAction = onClick;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    public static void RegisterPointerDown(this GameObject go, Action<PointerEventData, GameObject, object[]> onClickDown, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnPointerDownAction = onClickDown;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    public static void RegisterPointerUp(this GameObject go, Action<PointerEventData, GameObject, object[]> onClickUp, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnPointerUpAction = onClickUp;
        if (args != null)
        {
            listener.Args = args;
        }
    }

    public static void RegisterDrag(this GameObject go, Action<PointerEventData, GameObject, object[]> onDrag, params object[] args)
    {
        UIListener listener = go.GetOrAddComponent<UIListener>();
        if (listener == null)
        {
            listener = go.AddComponent<UIListener>();
        }
        listener.OnDragAction = onDrag;
        if (args != null)
        {
            listener.Args = args;
        }
    }
}