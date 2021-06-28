using FixedMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public class FixedCylinderCollider : FixedColliderBase
    {
        public FixedFloat Radius;

        public FixedCylinderCollider(FixedColliderConfig config)
        {
            Position = config.Position;
            Radius = config.Radius;
            Name = config.Name;
        }

        public void DetectCollision(List<FixedColliderBase> colliders, ref FixedVector3 velocity, ref FixedVector3 borderAdjust)
        {
            if (velocity == FixedVector3.Zero)
            {
                return;
            }

            FixedVector3 normal = FixedVector3.Zero;
            FixedVector3 adjust = FixedVector3.Zero;
            if (DetectCollision(colliders[0], ref normal, ref adjust))
            {
                this.Log("碰撞.");
            }
        }

        protected override bool DetectBoxCollision(FixedBoxCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {

        }

        protected override bool DetectSphereCollision(FixedCylinderCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust)
        {

        }
    }
}
