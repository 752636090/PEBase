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
        /// box¥Û–°
        /// </summary>
        public FixedVector3 Size;
        /// <summary>
        /// box÷·œÚ
        /// </summary>
        public FixedVector3[] Rotation;

        /// <summary>
        /// cylinder∞Îæ∂
        /// </summary>
        public FixedFloat Radius;
    }
}