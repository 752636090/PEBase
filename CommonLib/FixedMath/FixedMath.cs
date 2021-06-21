using System;

namespace FixedMath
{
    public struct FixedInt
    {
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

        const int BIT_MOVE_COUNT = 10; // 移位计数
        const long MULTIPLIER_FACTOR = 1 << BIT_MOVE_COUNT;

        public FixedInt(int val)
        {
            scaledValue = val * MULTIPLIER_FACTOR;
        }

        public FixedInt(float val)
        {
            scaledValue = (long)Math.Round(val * MULTIPLIER_FACTOR);
        }


    }
}
