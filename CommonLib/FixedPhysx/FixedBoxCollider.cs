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

        public override bool DetectBoxCollision(FixedBoxCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            // 分离轴算法
            this.LogError("TODO");
            return false;
        }

        public override bool DetectSphereCollision(FixedCylinderCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {
            FixedVector3 tmpNormal = FixedVector3.Zero;
            FixedVector3 tmpAdjust = FixedVector3.Zero;
            bool result = collider.DetectBoxCollision(this, ref tmpNormal, ref tmpAdjust);
            normal = -tmpNormal;
            borderAdjust = -tmpAdjust;
            return result;
        }
    }
}
