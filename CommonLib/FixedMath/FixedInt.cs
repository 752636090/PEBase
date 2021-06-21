using System;

namespace FixedMath
{
    public struct FixedInt
    {
        private static class Const
        {
            /// <summary>
            /// 移位计数
            /// </summary>
            public const int BitMoveCount = 10;
            public const long MultiplierFactor = 1 << BitMoveCount;
        }

        public static readonly FixedInt Zero = new FixedInt(0);
        public static readonly FixedInt One = new FixedInt(1);

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
        private FixedInt(long scaledValue)
        {
            this.scaledValue = scaledValue;
        }

        public FixedInt(int val)
        {
            scaledValue = val * Const.MultiplierFactor;
        }

        public FixedInt(float val)
        {
            scaledValue = (long)Math.Round(val * Const.MultiplierFactor);
        }

        /// <summary>
        /// float损失精度，必须显示转换
        /// </summary>
        /// <param name="f"></param>
        public static explicit operator FixedInt(float f)
        {
            return new FixedInt((long)Math.Round(f * Const.MultiplierFactor));
        }

        /// <summary>
        /// int不损失精度，可以隐式转换
        /// </summary>
        /// <param name="i"></param>
        public static implicit operator FixedInt(int i)
        {
            return new FixedInt(i);
        }
        #endregion

        #region 运算符
        #region 加减乘除、取反
        public static FixedInt operator +(FixedInt a, FixedInt b)
        {
            return new FixedInt(a.scaledValue + b.scaledValue);
        }

        public static FixedInt operator -(FixedInt a, FixedInt b)
        {
            return new FixedInt(a.scaledValue - b.scaledValue);
        }

        public static FixedInt operator *(FixedInt a, FixedInt b)
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
            return new FixedInt(value);
        }

        public static FixedInt operator /(FixedInt a, FixedInt b)
        {
            if (b.scaledValue == 0)
            {
                throw new Exception("除数等于0");
            }
            return new FixedInt((a.scaledValue << Const.BitMoveCount) / b.scaledValue);
        }

        public static FixedInt operator -(FixedInt value)
        {
            return new FixedInt(-value.scaledValue);
        }
        #endregion

        #region 比较、移位
        public static bool operator ==(FixedInt a, FixedInt b)
        {
            return a.scaledValue == b.scaledValue;
        }

        public static bool operator !=(FixedInt a, FixedInt b)
        {
            return a.scaledValue != b.scaledValue;
        }

        public static bool operator >(FixedInt a, FixedInt b)
        {
            return a.scaledValue > b.scaledValue;
        }

        public static bool operator <(FixedInt a, FixedInt b)
        {
            return a.scaledValue < b.scaledValue;
        }

        public static bool operator >=(FixedInt a, FixedInt b)
        {
            return a.scaledValue >= b.scaledValue;
        }

        public static bool operator <=(FixedInt a, FixedInt b)
        {
            return a.scaledValue <= b.scaledValue;
        }

        public static FixedInt operator >>(FixedInt value, int moveCount)
        {
            if (value.scaledValue >= 0)
            {
                return new FixedInt(value.scaledValue >> moveCount);
            }
            else
            {
                return new FixedInt(-(-value.scaledValue >> moveCount));
            }
        }

        public static FixedInt operator <<(FixedInt value, int moveCount)
        {
            return new FixedInt(value.scaledValue << moveCount);
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
            FixedInt vInt = (FixedInt)obj;
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
