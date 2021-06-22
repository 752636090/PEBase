using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKCPNet : MonoBehaviour
{
    private void Start()
    {
        KCPTest.KCPTest.Start();
    }

    private void Update()
    {
        KCPTest.KCPTest.Update();
    }
}
