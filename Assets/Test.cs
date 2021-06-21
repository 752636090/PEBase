using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool IsTestFixedMath;

    private void Awake()
    {
        if (IsTestFixedMath) gameObject.AddComponent<TestFixedMath>();
    }
}
