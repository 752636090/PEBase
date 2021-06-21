using System;

namespace FixedMath
{

    public struct FixedInt
    {
        static class Const
        {
            public const int BitMoveCount = 10; // 移位计数
            public const long MultiplierFactor = 1 << BitMoveCount;
        }

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

        #region 运算符
        public static FixedInt operator -(FixedInt value)
        {
            return new FixedInt(-value.scaledValue);
        }

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
            return new FixedInt(value.scaledValue >> moveCount);
        }

        public static FixedInt operator <<(FixedInt value, int moveCount)
        {
            return new FixedInt(value.scaledValue << moveCount);
        }
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

        public float RawInt
        {
            get
            {
                return (int)(scaledValue >> Const.BitMoveCount);
            }
        }

        public override string ToString()
        {
            return RawFloat.ToString();
        }
    }
}
