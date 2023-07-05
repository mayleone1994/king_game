using System;
using System.Collections.Generic;
using System.Linq;

namespace KingGame
{
    public static class Utils
    {
        public static void Shuffle<T>(this IList<T> collection) where T : class
        {
            Random random = new Random();

            int n = collection.Count;

            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + random.Next(n - i);
                T t = collection[r];
                collection[r] = collection[i];
                collection[i] = t;
            }
        }
    }
}