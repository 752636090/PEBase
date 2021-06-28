using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestFixedPhysx : MonoBehaviour
{
    private void Start()
    {
        Instantiate(Resources.Load("TestFixedPhysx"));

        CommonLog.InitSettings(new LogConfig { LogType = Utils.LogType.Unity });
        this.Log("初始化日志工具完毕.");

        Time.fixedDeltaTime = 0.667f;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
