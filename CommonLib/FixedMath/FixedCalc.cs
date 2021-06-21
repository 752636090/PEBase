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
    }
}
