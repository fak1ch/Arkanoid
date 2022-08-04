using UnityEngine;

public static class Utils
{
    public static bool CheckRandomless(int procent)
    {
        if (procent == 0)
            return false;

        return Random.Range(0, 100 / procent) == 0;
    }

    public static float GetProcent(float a, float b, float value)
    {
        if (b - a == 0)
            return 0;

        return (value - a) / (b - a);
    }
}
