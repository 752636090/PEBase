using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTestUIListener : WindowRoot
{
    public GameObject go;

    private void Start()
    {
        RegisterClick(go, (eventData) =>
        {
            print($"Click:{go.name}");
        });
    }
}
