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
        /// box��С
        /// </summary>
        public FixedVector3 Size { get; private set; }
        /// <summary>
        /// box����
        /// </summary>
        public FixedVector3[] Rotation { get; private set; }

        /// <summary>
        /// cylinder�뾶
        /// </summary>
        public FixedFloat Radius { get; private set; }
    }
}