using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random rng = new System.Random(seed);

        for (int i = 0; i < array.Length - 1; i++) //because you can skip last iteration of fisher yates shuffle
        {
            int randomIndex = rng.Next(i, array.Length); //following the algorithm
            T tempItem = array[randomIndex];            //tmp item to store the value so we can switch the index of the items
            array[randomIndex] = array[i];              //switching the index of [i] to random index
            array[i] = tempItem;                        //moving the random index item leftwards to [i]
        }

        return array;
    }

    #region Map explanation
    //how map works is that value will be "clamped" according to the speed and range.
    //so in this case, if the distance is more than the range, f = max speed;
    //if the distance is < 0, the speed will also be 0
    //but if not, (meaning it's > 0 but < range) f = slower speed the smaller the range is.
    #endregion
    public static float Map(float value, float from, float to, float from2, float to2)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}
