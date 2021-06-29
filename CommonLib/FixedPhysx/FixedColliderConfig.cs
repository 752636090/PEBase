using FixedMath;
using System.Collections;
using System.Collections.Generic;

namespace FixedPhysx
{
    public class FixedColliderConfig
    {
        public string Name;
        public FixedColliderType Type;
        public FixedVector3 Position;

        /// <summary>
        /// box��С
        /// </summary>
        public FixedVector3 Size;
        /// <summary>
        /// box����
        /// </summary>
        public FixedVector3[] Rotation;

        /// <summary>
        /// cylinder�뾶
        /// </summary>
        public FixedFloat Radius;
    }
}