using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
#if UNITY_ENV
using UnityEngine;
#endif

namespace FixedMath
{
    public struct FixedVector3
    {
        public FixedInt X;
        public FixedInt Y;
        public FixedInt Z;

        public FixedVector3(FixedInt x, FixedInt y, FixedInt z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

#if UNITY_ENV
        public FixedVector3(Vector3 v)
        {
            X = (FixedInt)v.x;
            Y = (FixedInt)v.y;
            Z = (FixedInt)v.z;
        }
#endif

        public FixedInt this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                }
            }
        }

        #region 定义常用向量
        public static FixedVector3 Zero
        {
            get
            {
                return new FixedVector3(0, 0, 0);
            }
        }
        public static FixedVector3 One
        {
            get
            {
                return new FixedVector3(1, 1, 1);
            }
        }
        public static FixedVector3 Forward
        {
            get
            {
                return new FixedVector3(0, 0, 1);
            }
        }
        public static FixedVector3 Back
        {
            get
            {
                return new FixedVector3(0, 0, -1);
            }
        }
        public static FixedVector3 Left
        {
            get
            {
                return new FixedVector3(-1, 0, 0);
            }
        }
        public static FixedVector3 Right
        {
            get
            {
                return new FixedVector3(1, 0, 0);
            }
        }
        public static FixedVector3 Up
        {
            get
            {
                return new FixedVector3(0, 1, 0);
            }
        }
        public static FixedVector3 Down
        {
            get
            {
                return new FixedVector3(0, -1, 0);
            }
        }
        #endregion

#if UNITY_ENV
        /// <summary>
        /// 获取浮点数向量（注意：不可再进行逻辑运算）
        /// </summary>
        /// <returns></returns>
        public Vector3 ConvertViewVector()
        {
            return new Vector3(X.RawFloat, Y.RawFloat, Z.RawFloat);
        }
#endif

        public long[] ConvertLongArray()
        {
            return new long[] { X.ScaleValue, Y.ScaleValue, Z.ScaleValue };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            FixedVector3 v = (FixedVector3)obj;
            return v.X == X && v.Y == Y && v.Z == Z;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }

        public override string ToString()
        {
            return $"x:{X} y:{Y} z:{Z}";
        }
    }
}
