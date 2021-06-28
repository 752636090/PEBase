using System;
using System.Collections.Generic;
using System.Text;

namespace FixedMath
{
    public class FixedCalc
    {
        public static FixedFloat Sqrt(FixedFloat value, int iteratorCount = 8)
        {
            if (value == FixedFloat.Zero)
            {
                return 0;
            }
            if (value < FixedFloat.Zero)
            {
                throw new Exception("被平方数小于0");
            }
            FixedFloat result = value;
            FixedFloat history;
            int count = 0;
            do
            {
                history = result;
                result = (result + value / result) >> 1;
                ++count;
            } while (result != history && count < iteratorCount);
            return result;
        }

        public static FixedArgs Acos(FixedFloat value)
        {
            FixedFloat rate = (value * AcosTable.HalfIndexCount) + AcosTable.HalfIndexCount;
            rate = Clamp(rate, FixedFloat.Zero, AcosTable.IndexCount);
            int rad = AcosTable.Table[rate.RawInt];
            return new FixedArgs(rad, AcosTable.Multiplier);
        }

        public static FixedFloat Clamp(FixedFloat input, FixedFloat min, FixedFloat max)
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
