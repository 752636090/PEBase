using FixedMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public abstract class FixedColliderBase
    {
        public string Name;
        public FixedVector3 Position;


        protected virtual bool DetectCollision(FixedColliderBase collider, ref FixedVector3 velocity, ref FixedVector3 borderAdjust)
        {
            if (collider is FixedBoxCollider boxCollider)
            {
                return DetectBoxCollision(boxCollider, ref velocity, ref borderAdjust);
            }
            else if (collider is FixedCylinderCollider cylinderCollider)
            {
                return DetectSphereCollision(cylinderCollider, ref velocity, ref borderAdjust);
            }
            else
            {
                this.LogError("TODO"); // TODO
                return false;
            }
        }
        public abstract bool DetectSphereCollision(FixedCylinderCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust);
        public abstract bool DetectBoxCollision(FixedBoxCollider collider, ref FixedVector3 normal, ref FixedVector3 borderAdjust);
        //public abstract void DetectCollision(List<FixedColliderBase> colliders, ref FixedVector3 velocity, ref FixedVector3 borderAdjust);
    }
}
