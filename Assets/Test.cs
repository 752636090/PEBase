using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTestFixedMath;
    public bool IsTestKCPNet;

    private void Awake()
    {
        if (IsTestFixedMath) gameObject.AddComponent<TestFixedMath>();
        if (IsTestKCPNet) gameObject.AddComponent<TestKCPNet>();
    }
}
