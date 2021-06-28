using FixedMath;
using FixedPhysx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TestFixedPhysx : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private FixedVector3 logicPos;
    private FixedVector3 logicDir;
    [SerializeField]
    private int logicSpeed = 1;
    [SerializeField]
    private float multiplier = 0.1f;
    private FixedVector3 inputDir;
    private FixedCylinderCollider playerCollider;

    private void Start()
    {
        GameObject testRoot = Instantiate(Resources.Load("TestFixedPhysx")) as GameObject;
        testRoot.name = "TestFixedPhysx";

        CommonLog.InitSettings(new LogConfig { LogType = Utils.LogType.Unity });
        this.Log("初始化日志工具完毕.");

        Time.fixedDeltaTime = 0.0667f;

        player = testRoot.transform.Find("Player").transform;
        InitPlayer();
    }

    private void FixedUpdate()
    {
        GetInput();
        FixedVector3 moveDir = inputDir;
        playerCollider.Position += moveDir * logicSpeed * (FixedFloat)multiplier;

        logicPos = playerCollider.Position;
        player.position = logicPos.ConvertViewVector3();
    }

    private void GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDir = new FixedVector3(new Vector3(h, 0, v).normalized);
    }

    private void InitPlayer()
    {
        FixedColliderConfig config = new FixedColliderConfig
        {
            Name = "player",
            Position = new FixedVector3(player.position),
            Type = FixedColliderType.Cylinder,
            Radius = (FixedFloat)(player.localScale.x / 2)
        };
        playerCollider = new FixedCylinderCollider(config);
        logicPos = config.Position;
        logicDir = FixedVector3.Zero;
    }
}
