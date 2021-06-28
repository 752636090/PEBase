using System;

namespace FixedMath
{
    public struct FixedFloat
    {
        private static class Const
        {
            /// <summary>
            /// 移位计数
            /// </summary>
            public const int BitMoveCount = 10;
            public const long MultiplierFactor = 1 << BitMoveCount;
        }

        public static readonly FixedFloat Zero = new FixedFloat(0);
        public static readonly FixedFloat One = new FixedFloat(1);

        private long scaledValue;
        public long ScaleValue
        {
            get
            {
                return scaledValue;
            }
            set
            {
                scaledValue = value;
            }
        }

        #region 构造函数
        /// <summary>
        /// 内部使用，已经缩放完成的数据
        /// </summary>
        /// <param name="scaledValue"></param>
        private FixedFloat(long scaledValue)
        {
            this.scaledValue = scaledValue;
        }

        public FixedFloat(int val)
        {
            scaledValue = val * Const.MultiplierFactor;
        }

        public FixedFloat(float val)
        {
            scaledValue = (long)Math.Round(val * Const.MultiplierFactor);
        }

        /// <summary>
        /// float损失精度，必须显示转换
        /// </summary>
        /// <param name="f"></param>
        public static explicit operator FixedFloat(float f)
        {
            return new FixedFloat((long)Math.Round(f * Const.MultiplierFactor));
        }

        /// <summary>
        /// int不损失精度，可以隐式转换
        /// </summary>
        /// <param name="i"></param>
        public static implicit operator FixedFloat(int i)
        {
            return new FixedFloat(i);
        }
        #endregion

        #region 运算符
        #region 加减乘除、取反
        public static FixedFloat operator +(FixedFloat a, FixedFloat b)
        {
            return new FixedFloat(a.scaledValue + b.scaledValue);
        }

        public static FixedFloat operator -(FixedFloat a, FixedFloat b)
        {
            return new FixedFloat(a.scaledValue - b.scaledValue);
        }

        public static FixedFloat operator *(FixedFloat a, FixedFloat b)
        {
            long value = a.scaledValue * b.scaledValue;
            if (value >= 0)
            {
                value >>= Const.BitMoveCount;
            }
            else
            {
                value = -(-value >> Const.BitMoveCount);
            }
            return new FixedFloat(value);
        }

        public static FixedFloat operator /(FixedFloat a, FixedFloat b)
        {
            if (b.scaledValue == 0)
            {
                throw new Exception("除数等于0");
            }
            return new FixedFloat((a.scaledValue << Const.BitMoveCount) / b.scaledValue);
        }

        public static FixedFloat operator -(FixedFloat value)
        {
            return new FixedFloat(-value.scaledValue);
        }
        #endregion

        #region 比较、移位
        public static bool operator ==(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue == b.scaledValue;
        }

        public static bool operator !=(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue != b.scaledValue;
        }

        public static bool operator >(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue > b.scaledValue;
        }

        public static bool operator <(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue < b.scaledValue;
        }

        public static bool operator >=(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue >= b.scaledValue;
        }

        public static bool operator <=(FixedFloat a, FixedFloat b)
        {
            return a.scaledValue <= b.scaledValue;
        }

        public static FixedFloat operator >>(FixedFloat value, int moveCount)
        {
            if (value.scaledValue >= 0)
            {
                return new FixedFloat(value.scaledValue >> moveCount);
            }
            else
            {
                return new FixedFloat(-(-value.scaledValue >> moveCount));
            }
        }

        public static FixedFloat operator <<(FixedFloat value, int moveCount)
        {
            return new FixedFloat(value.scaledValue << moveCount);
        } 
        #endregion
        #endregion

        /// <summary>
        /// 转换完成后，不可再参与逻辑运算。只是方便查看数据
        /// </summary>
        public float RawFloat
        {
            get
            {
                return scaledValue * 1.0f / Const.MultiplierFactor;
            }
        }

        public int RawInt
        {
            get
            {
                if (scaledValue >= 0)
                {
                    return (int)(scaledValue >> Const.BitMoveCount);
                }
                else
                {
                    return -(int)(-scaledValue >> Const.BitMoveCount); // 解决负数是补码的问题
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            FixedFloat vInt = (FixedFloat)obj;
            return scaledValue == vInt.scaledValue;
        }

        public override int GetHashCode()
        {
            return RawFloat.GetHashCode();
        }

        public override string ToString()
        {
            return RawFloat.ToString();
        }
    }
}
