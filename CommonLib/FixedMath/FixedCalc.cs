using System;
using System.Collections.Generic;
using System.Text;

namespace FixedMath
{
    public class FixedCalc
    {
        public static FixedInt Sqrt(FixedInt value, int iteratorCount = 8)
        {
            if (value == FixedInt.Zero)
            {
                return 0;
            }
            if (value < FixedInt.Zero)
            {
                throw new Exception("被平方数小于0");
            }
            FixedInt result = value;
            FixedInt history;
            int count = 0;
            do
            {
                history = result;
                result = (result + value / result) >> 1;
                ++count;
            } while (result != history && count < iteratorCount);
            return result;
        }

        public static FixedArgs Acos(FixedInt value)
        {
            FixedInt rate = (value * AcosTable.HalfIndexCount) + AcosTable.HalfIndexCount;
            rate = Clamp(rate, FixedInt.Zero, AcosTable.IndexCount);
            int rad = AcosTable.Table[rate.RawInt];
            return new FixedArgs(rad, AcosTable.Multiplier);
        }

        public static FixedInt Clamp(FixedInt input, FixedInt min, FixedInt max)
        {
            if (input < min)
            {
                return min;
            }
            if (input > max)
            {
                return max;
            }
            return input;
        }
    }
}
