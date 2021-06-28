using FixedMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FixedPhysx
{
    public class FixedColliderConfig
    {
        public string Name { get; private set; }
        public FixedColliderType Type { get; private set; }
        public FixedVector3 Position { get; private set; }

        /// <summary>
        /// box¥Û–°
        /// </summary>
        public FixedVector3 Size { get; private set; }
        /// <summary>
        /// box÷·œÚ
        /// </summary>
        public FixedVector3[] Rotation { get; private set; }

        /// <summary>
        /// cylinder∞Îæ∂
        /// </summary>
        public FixedFloat Radius { get; private set; }
    }
}