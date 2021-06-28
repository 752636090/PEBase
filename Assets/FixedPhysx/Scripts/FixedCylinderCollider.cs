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
    }
}
