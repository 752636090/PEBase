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
        public FixedFloat X;
        public FixedFloat Y;
        public FixedFloat Z;

        public FixedVector3(FixedFloat x, FixedFloat y, FixedFloat z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

#if UNITY_ENV
        public FixedVector3(Vector3 v)
        {
            X = (FixedFloat)v.x;
            Y = (FixedFloat)v.y;
            Z = (FixedFloat)v.z;
        }
#endif

        public FixedFloat this[int index]
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

        #region 运算符
        public static FixedVector3 operator +(FixedVector3 v1, FixedVector3 v2)
        {
            FixedFloat x = v1.X + v2.X;
            FixedFloat y = v1.Y + v2.Y;
            FixedFloat z = v1.Z + v2.Z;
            return new FixedVector3(x, y, z);
        }
        public static FixedVector3 operator -(FixedVector3 v1, FixedVector3 v2)
        {
            FixedFloat x = v1.X - v2.X;
            FixedFloat y = v1.Y - v2.Y;
            FixedFloat z = v1.Z - v2.Z;
            return new FixedVector3(x, y, z);
        }
        public static FixedVector3 operator *(FixedVector3 v, FixedFloat value)
        {
            FixedFloat x = v.X * value;
            FixedFloat y = v.Y * value;
            FixedFloat z = v.Z * value;
            return new FixedVector3(x, y, z);
        }
        public static FixedVector3 operator *(FixedFloat value, FixedVector3 v)
        {
            FixedFloat x = v.X * value;
            FixedFloat y = v.Y * value;
            FixedFloat z = v.Z * value;
            return new FixedVector3(x, y, z);
        }
        public static FixedVector3 operator /(FixedVector3 v, FixedFloat value)
        {
            FixedFloat x = v.X / value;
            FixedFloat y = v.Y / value;
            FixedFloat z = v.Z / value;
            return new FixedVector3(x, y, z);
        }
        public static FixedVector3 operator -(FixedVector3 v)
        {
            FixedFloat x = -v.X;
            FixedFloat y = -v.Y;
            FixedFloat z = -v.Z;
            return new FixedVector3(x, y, z);
        }
        public static bool operator ==(FixedVector3 v1, FixedVector3 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static bool operator !=(FixedVector3 v1, FixedVector3 v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
        }
        #endregion

        /// <summary>
        /// 当前向量长度平方
        /// </summary>
        public FixedFloat SqrMagnitude
        {
            get
            {
                return X * X + Y * Y + Z * Z;
            }
        }

        public static FixedFloat SqrMagnitube(FixedVector3 v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        public FixedFloat Magnitude
        {
            get
            {
                return FixedCalc.Sqrt(SqrMagnitude);
            }
        }

        /// <summary>
        /// 返回当前定点向量的单位向量
        /// </summary>
        public FixedVector3 Normalized
        {
            get
            {
                if (Magnitude > 0)
                {
                    FixedFloat rate = FixedFloat.One / Magnitude;
                    return new FixedVector3(X * rate, Y * rate, Z * rate);
                }
                else
                {
                    return Zero;
                }
            }
        }

        /// <summary>
        /// 返回传入参数向量的单位向量
        /// </summary>
        public static FixedVector3 Normalize(FixedVector3 v)
        {
            return v.Normalized;
        }

        /// <summary>
        /// 规格化当前向量为单位向量
        /// </summary>
        public void Normalize()
        {
            if (Magnitude > 0)
            {
                FixedFloat rate = FixedFloat.One / Magnitude;
                X *= rate;
                Y *= rate;
                Z *= rate;
            }
        }

        /// <summary>
        /// 点乘
        /// </summary>
        public static FixedFloat Dot(FixedVector3 a, FixedVector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        public static FixedVector3 Cross(FixedVector3 a, FixedVector3 b)
        {
            return new FixedVector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// 向量夹角
        /// </summary>
        public static FixedArgs Angle(FixedVector3 from, FixedVector3 to)
        {
            FixedFloat dot = Dot(from, to);
            FixedFloat mod = from.Magnitude * to.Magnitude;
            if (mod == 0)
            {
                return FixedArgs.Zero;
            }
            FixedFloat value = dot / mod;
            // 反余弦函数计算
            return FixedCalc.Acos(value);
        }

#if UNITY_ENV
        /// <summary>
        /// 获取浮点数向量（注意：不可再进行逻辑运算）
        /// </summary>
        /// <returns></returns>
        public Vector3 ConvertViewVector3()
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
