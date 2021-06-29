using FixedMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixedPhysx
{
    public class CollisionInfo
    {
        public FixedColliderBase Collider;
        public FixedVector3 Normal;
        public FixedVector3 BorderAdjust;
    }
}
