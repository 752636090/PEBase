using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIListener : MonoBehaviour
{
    private void Start()
    {
        GameObject testUI = Instantiate(Resources.Load("UITestUIListener"), GameObject.Find("Canvas/Root").transform) as GameObject;
    }
}
