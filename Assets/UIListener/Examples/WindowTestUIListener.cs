using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowTestUIListener : WindowRoot
{
    public GameObject go;

    private void Start()
    {
        //RegisterPointerClick(go, (eventData, args) =>
        //{
        //    print($"Click:{go.name}");
        //});
        //RegisterPointerClick(go, ClickItem, go);
        RegisterPointerClick(go, ClickItem, go, 1);
        go.RegisterPointerDown(DownItem, 2);
        go.RegisterDrag(DragItem, 3);
        go.RegisterPointerUp(UpItem, 4);
    }

    //private void ClickItem(PointerEventData eventData, object[] args)
    //{
    //    print($"Click:{(args[0] as GameObject).name} {(int)args[1]}");
    //}
    private void ClickItem(PointerEventData eventData, GameObject go, object[] args)
    {
        print($"Click:{go.name} {(int)args[0]}");
    }

    private void DownItem(PointerEventData eventData, GameObject go, object[] args)
    {
        print($"Down:{go.name} {(int)args[0]}");
    }

    private void DragItem(PointerEventData eventData, GameObject go, object[] args)
    {
        print($"Drag:{go.name} {(int)args[0]}");
    }

    private void UpItem(PointerEventData eventData, GameObject go, object[] args)
    {
        print($"Up:{go.name} {(int)args[0]}");
    }
}
