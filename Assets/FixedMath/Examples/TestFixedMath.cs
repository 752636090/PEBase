using FixedMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFixedMath : MonoBehaviour
{
    private void Start()
    {
        Example7();
    }

    private static void Example1()
    {
        FixedFloat val1 = new FixedFloat(1);
        FixedFloat val2 = new FixedFloat(0.5f);

        print(val1 > val2);

        FixedFloat val3 = val1 << 1;
        print(val3.ToString());

        FixedFloat val4 = 1;
        FixedFloat val5 = (FixedFloat)0.5f;
    }

    private static void Example2()
    {
        FixedFloat val1 = new FixedFloat(1);
        FixedFloat val2 = new FixedFloat(1.5f);
        print((val1 * val2).ToString());

        FixedFloat val3 = new FixedFloat(2);
        FixedFloat val4 = new FixedFloat(0.5f);
        print((val3 / val4).ToString());
    }

    private static void Example3()
    {
        int hp = 500;
        FixedFloat val1 = hp * new FixedFloat(0.3f);
        print($"before scale:{val1.ScaleValue}");
        print($"before float:{val1.RawFloat}");
        print($"before int:{val1.RawInt}");
        print("----------------------------");

        FixedFloat val2 = hp * new FixedFloat(-0.3f);
        print($"after scale:{val2.ScaleValue}");
        print($"after float:{val2.RawFloat}");
        print($"after int:{val2.RawInt}");
    }

    private static void ExampleV3()
    {
        //FixedVector3 v1 = new FixedVector3(1, 2, 3);
        FixedVector3 v1 = new FixedVector3(new Vector3(1, 1.5f, 2.3f));
        print(v1.ToString());
    }

    private static void Example4()
    {
        FixedVector3 v = new FixedVector3(2, 2, 2);
        print(v.Magnitude);
        print("----------------------------");

        FixedFloat val = 3;
        print(FixedCalc.Sqrt(val));
    }

    private static void Example5()
    {
        FixedVector3 v = new FixedVector3(2, 2, 2);
        print($"a:{v.Normalized}");
        print($"b:{FixedVector3.Normalize(v)}");
        print($"c:{v}");
        v.Normalize();
        print($"c:{v}");
    }

    private static void Example6()
    {
        FixedVector3 v1 = new FixedVector3(1, 0, 0);
        FixedVector3 v2 = new FixedVector3(1, 1, 0);
        FixedArgs angle = FixedVector3.Angle(v1, v2);
        print($"angle view:{angle.ConvertViewAngle()}");
        print($"angle float:{angle.ConvertToFloat()}");
        print($"angle info:{angle}");

        FixedVector3 v3 = new FixedVector3(1, 0, 0);
        FixedVector3 v4 = new FixedVector3(1, (FixedFloat)1.732f, 0);
        FixedArgs angle2 = FixedVector3.Angle(v3, v4);
        print($"angle view:{angle2.ConvertViewAngle()}");
        print($"angle float:{angle2.ConvertToFloat()}");
        print($"angle info:{angle2}");
    }

    private static void Example7()
    {
        FixedVector3 v1 = new FixedVector3(Vector3.right);
        FixedVector3 v2 = new FixedVector3(Vector3.up);

        print(FixedVector3.Angle(v1, v2).ConvertViewAngle());
    }
}
