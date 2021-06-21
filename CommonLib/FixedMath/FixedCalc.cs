using System;
using System.Collections.Generic;
using System.Text;

namespace FixedMath
{
    public class FixedCalc
    {
        public static FixedInt Sqrt(FixedInt value, int interatorCount = 8)
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
            for (int i = 0; i < interatorCount; i++)
            {
                result = (result + value / result) >> 1;
                //UnityEngine.Debug.Log($"迭代{i}次，result={result.RawFloat}");
            }
            return result;
        }
    }
}
