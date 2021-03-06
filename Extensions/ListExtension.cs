﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Extensions
{
    public static class ListExtension
    {

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, int seed)
        {
        Random rng = new Random(seed);

        int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this Stack<T> stack) {
            IList<T> temp = stack.ToList();
            temp.Shuffle();
            stack.Clear();
            foreach (var item in temp)
            {
                stack.Push(item);
            }
        }
    }
}
