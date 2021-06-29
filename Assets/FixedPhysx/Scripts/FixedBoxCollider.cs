using FixedMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public class FixedBoxCollider : FixedColliderBase
    {
        public FixedVector3 Size;
        public FixedVector3[] Rotation;

        public FixedBoxCollider(FixedColliderConfig config)
        {
            Position = config.Position;
            Size = config.Size;
            Rotation = new FixedVector3[3];
            Rotation[0] = config.Rotation[0];
            Rotation[1] = config.Rotation[1];
            Rotation[2] = config.Rotation[2];
            Name = config.Name;
        }

        protected override bool DetectBoxCollision(FixedBoxCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            this.LogError("TODO"); // 分离轴算法
            return false;
        }

        protected override bool DetectSphereCollision(FixedCylinderCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            this.LogError("TODO");
            return false;
        }
    }
}
