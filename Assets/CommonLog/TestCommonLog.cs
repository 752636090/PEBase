using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

public class TestCommonLog : MonoBehaviour
{
    private void Start()
    {
        LogConfig config = new LogConfig
        {
            LogType = Utils.LogType.Unity,
            SavePath = Application.persistentDataPath + "/CommonLog/",
            SaveName = "ClientCommonLog.txt"
        };
        CommonLog.InitSettings(config);

        CommonLog.Log("{0} START...", "ServerCommonLog");
        CommonLog.ColorLog(ConsoleColor.Red, "Color Log:{0}", ConsoleColor.Red.ToString());
        CommonLog.ColorLog(ConsoleColor.DarkGreen, "Color Log:{0}", ConsoleColor.DarkGreen.ToString());
        CommonLog.ColorLog(ConsoleColor.Blue, "Color Log:{0}", ConsoleColor.Blue.ToString());
        CommonLog.ColorLog(ConsoleColor.Cyan, "Color Log:{0}", ConsoleColor.Cyan.ToString());
        CommonLog.ColorLog(ConsoleColor.Magenta, "Color Log:{0}", ConsoleColor.Magenta.ToString());
        CommonLog.ColorLog(ConsoleColor.DarkYellow, "Color Log:" + ConsoleColor.DarkYellow.ToString());

        Root root = new Root();
        root.Init();

        //System.Diagnostics.Process.Start("explorer.exe", Application.persistentDataPath);
        print(Application.persistentDataPath);
    }
}

class Root
{
    public void Init()
    {
        this.Log("InitRootLog.");
        Mgr mgr = new Mgr();
        mgr.Init();
    }
}
class Mgr
{
    public void Init()
    {
        this.Warning("Init Mgr Waring.");
        Item item = new Item();
        item.Init();
    }
}
class Item
{
    public void Init()
    {
        this.Error("Init Item Error");
        this.Trace("Trace This Func");
    }
}