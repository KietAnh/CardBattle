using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class RandomExtensions
{
    public static int RandomDependOnProbability(List<float> probs)
    {
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        float x = (float) rand.NextDouble();
        float a = 0;
        float b;
        for (int i = 0; i < probs.Count; i++)
        {
            float prob = probs[i];
            b = a + prob;
            if (a <= x && x < b)
            {
                return i;
            }
            a = b;
        }
        return 0;
    }

    public static float RandomRange(float min, float max)
    {
        System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
        return (float)rand.NextDouble() * (max - min) + min;
    }
}
