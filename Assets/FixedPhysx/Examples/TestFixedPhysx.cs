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
    [SerializeField]
    private Transform transEnvRoot;
    private FixedVector3 logicPos;
    private FixedVector3 logicDir;
    [SerializeField]
    private int logicSpeed = 1;
    [SerializeField]
    private float multiplier = 0.1f;
    private FixedVector3 inputDir;
    private FixedCylinderCollider playerCollider;
    private FixedEnvColliders logicEnv;

    private void Start()
    {
        GameObject testRoot = Instantiate(Resources.Load("TestFixedPhysx")) as GameObject;
        testRoot.name = "TestFixedPhysx";
        transEnvRoot = testRoot.transform.GetChild(0);

        CommonLog.InitSettings(new LogConfig { LogType = Utils.LogType.Unity });
        this.Log("初始化日志工具完毕.");

        Time.fixedDeltaTime = 0.0667f;

        player = testRoot.transform.Find("Player").transform;
        InitEnv();
        InitPlayer();
    }

    private void FixedUpdate()
    {
        GetInput();
        FixedVector3 moveDir = inputDir;
        playerCollider.Position += moveDir * logicSpeed * (FixedFloat)multiplier;
        FixedVector3 borderAdjust = FixedVector3.Zero;
        playerCollider.DetectCollision(logicEnv.EnvColliderLst, ref moveDir, ref borderAdjust);

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

    private void InitEnv()
    {
        List<FixedColliderConfig> envColliderConfigLst = GenerateEnvColliderConfigs();
        logicEnv = new FixedEnvColliders(envColliderConfigLst);
        logicEnv.Init();
    }

    private List<FixedColliderConfig> GenerateEnvColliderConfigs()
    {
        List<FixedColliderConfig> envColliderConfigLst = new List<FixedColliderConfig>();
        BoxCollider[] boxArr = transEnvRoot.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < boxArr.Length; i++)
        {
            Transform trans = boxArr[i].transform;
            if (trans.gameObject.activeSelf == false)
            {
                continue;
            }
            FixedColliderConfig config = new FixedColliderConfig
            {
                Position = new FixedVector3(trans.position)
            };

            config.Name = trans.gameObject.name;
            config.Size = new FixedVector3(trans.localScale / 2);
            config.Type = FixedColliderType.Box;
            config.Rotation = new FixedVector3[3];
            config.Rotation[0] = new FixedVector3(trans.right);
            config.Rotation[1] = new FixedVector3(trans.up);
            config.Rotation[2] = new FixedVector3(trans.forward);
            envColliderConfigLst.Add(config);
        }

        CapsuleCollider[] cylinderArr = transEnvRoot.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < cylinderArr.Length; i++)
        {
            Transform trans = cylinderArr[i].transform;
            FixedColliderConfig config = new FixedColliderConfig
            {
                Position = new FixedVector3(trans.position)
            };
            config.Name = trans.gameObject.name;
            config.Type = FixedColliderType.Cylinder;
            config.Radius = (FixedFloat)trans.localScale.x / 2;
            envColliderConfigLst.Add(config);
        }
        return envColliderConfigLst;
    }
}
