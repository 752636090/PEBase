using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogTest
{
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
}
