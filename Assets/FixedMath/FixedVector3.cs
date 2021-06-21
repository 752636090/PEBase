using System;
using System.Collections.Generic;
using System.Text;

namespace FixedMath
{
    public struct FixedVector3
    {
        public FixedInt X;
        public FixedInt Y;
        public FixedInt Z;

        public FixedVector3(FixedInt x, FixedInt y, FixedInt z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public FixedInt this[int index]
        {
            get
            {
                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => 0,
                };
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
    }
}
