using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTestFixedMath;
    public bool IsTestKCPNet;
    public bool IsTestCommonLog;
    public bool IsTestUIListener;

    private void Awake()
    {
        if (IsTestFixedMath) gameObject.AddComponent<TestFixedMath>();
        if (IsTestKCPNet) gameObject.AddComponent<TestKCPNet>();
        if (IsTestCommonLog) gameObject.AddComponent<TestCommonLog>();
        if (IsTestUIListener) gameObject.AddComponent<TestUIListener>();
    }
}
