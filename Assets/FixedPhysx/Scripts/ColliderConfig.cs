using FixedMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FixedPhysx
{
    public class ColliderConfig
    {
        public string Name { get; private set; }
        public ColliderType Type { get; private set; }
        public FixedVector3 Position { get; private set; }

        /// <summary>
        /// box¥Û–°
        /// </summary>
        public FixedVector3 Size { get; private set; }
        /// <summary>
        /// box÷·œÚ
        /// </summary>
        public FixedVector3[] Axis { get; private set; }

        /// <summary>
        /// cylinder∞Îæ∂
        /// </summary>
        public FixedInt Radius { get; private set; }
    }
}