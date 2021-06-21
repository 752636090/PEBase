using System;
using System.Collections.Generic;
using System.Text;

namespace FixedMath
{
    public struct FixedArgs
    {
        public int Value;
        public uint Multiplier;
        
        public FixedArgs(int value, uint multiplier)
        {
            Value = value;
            Multiplier = multiplier;
        }

        public static FixedArgs Zero = new FixedArgs(0, 10000);
        public static FixedArgs HalfPi = new FixedArgs(15708, 10000);
        public static FixedArgs PI = new FixedArgs(31416, 10000);
        public static FixedArgs TwoPi = new FixedArgs(62832, 10000);

        #region 运算符
        public static bool operator >(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value > b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        }
        public static bool operator <(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value < b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        }
        public static bool operator >=(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value >= b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        }
        public static bool operator <=(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value <= b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        }
        public static bool operator ==(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value == b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        }
        public static bool operator !=(FixedArgs a, FixedArgs b)
        {
            if (a.Multiplier == b.Multiplier)
            {
                return a.Value != b.Value;
            }
            else
            {
                throw new System.Exception("Multiplier is unequal");
            }
        } 
        #endregion

        /// <summary>
        /// 转化为视图角度，不可再用于逻辑运算
        /// </summary>
        /// <returns></returns>
        public int ConvertViewAngle()
        {
            float radians = ConvertToFloat();
            return (int)Math.Round(radians / Math.PI * 180);
        }

        /// <summary>
        /// 转化为视图弧度，不可再用于逻辑运算
        /// </summary>
        /// <returns></returns>
        public float ConvertToFloat()
        {
            return Value * 1.0f / Multiplier;
        }

        public override bool Equals(object obj)
        {
            return obj is FixedArgs args
                && Value == args.Value
                && Multiplier == args.Multiplier;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"value:{Value} multiplier:{Multiplier}";
        }
    }
}
