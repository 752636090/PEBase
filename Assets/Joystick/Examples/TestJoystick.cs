using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJoystick : MonoBehaviour
{
    private void Start()
    {
        GameObject testUI = Instantiate(Resources.Load("UIPrefab/UITestJoystick"), GameObject.Find("Canvas/Root").transform) as GameObject;
    }
}
