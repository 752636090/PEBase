using FixedMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFixedMath : MonoBehaviour
{
    private void Start()
    {
        Example3();
    }

    private static void Example1()
    {
        FixedInt val1 = new FixedInt(1);
        FixedInt val2 = new FixedInt(0.5f);

        print(val1 > val2);

        FixedInt val3 = val1 << 1;
        print(val3.ToString());

        FixedInt val4 = 1;
        FixedInt val5 = (FixedInt)0.5f;
    }

    private static void Example2()
    {
        FixedInt val1 = new FixedInt(1);
        FixedInt val2 = new FixedInt(1.5f);
        print((val1 * val2).ToString());

        FixedInt val3 = new FixedInt(2);
        FixedInt val4 = new FixedInt(0.5f);
        print((val3 / val4).ToString());
    }

    private static void Example3()
    {
        int hp = 500;
        FixedInt val1 = hp * new FixedInt(0.3f);
        print($"before scale:{val1.ScaleValue}");
        print($"before float:{val1.RawFloat}");
        print($"before int:{val1.RawInt}");
        print("----------------------------");

        FixedInt val2 = hp * new FixedInt(-0.3f);
        print($"after scale:{val2.ScaleValue}");
        print($"after float:{val2.RawFloat}");
        print($"after int:{val2.RawInt}");
    }
}
