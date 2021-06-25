using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowTestUIListener : WindowRoot
{
    public GameObject go;

    private void Start()
    {
        //RegisterClick(go, (eventData, args) =>
        //{
        //    print($"Click:{go.name}");
        //});
        //RegisterClick(go, ClickItem, go);
        RegisterClick(go, ClickItem, go, 123);
    }

    private void ClickItem(PointerEventData eventData, object[] args)
    {
        print($"Click:{(args[0] as GameObject).name} {(int)args[1]}");
    }
}
