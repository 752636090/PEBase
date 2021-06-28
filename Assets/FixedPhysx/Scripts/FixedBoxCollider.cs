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
            Rotation[0] = config.Rotation[0];
            Rotation[1] = config.Rotation[1];
            Rotation[2] = config.Rotation[2];
            Name = config.Name;
        }
    }
}
