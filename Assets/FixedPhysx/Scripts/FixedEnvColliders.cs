using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public class FixedEnvColliders
    {
        private readonly List<FixedColliderConfig> envColliderConfigLst;

        public List<FixedColliderBase> EnvColliderLst { get; private set; }

        public FixedEnvColliders(List<FixedColliderConfig> envColliderConfigLst)
        {
            this.envColliderConfigLst = envColliderConfigLst;
        }

        public void Init()
        {
            EnvColliderLst = new List<FixedColliderBase>();
            for (int i = 0; i < envColliderConfigLst.Count; i++)
            {
                FixedColliderConfig config = envColliderConfigLst[i];
                if (config.Type == FixedColliderType.Box)
                {
                    EnvColliderLst.Add(new FixedBoxCollider(config));
                }
                else if (config.Type == FixedColliderType.Cylinder)
                {
                    EnvColliderLst.Add(new FixedCylinderCollider(config));
                }
                else
                {
                    UnityEngine.Debug.Log("TODO"); // TODO
                }
            }
        }
    }
}
