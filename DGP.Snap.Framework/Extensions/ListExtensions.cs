﻿using System;
using System.Collections.Generic;

namespace DGP.Snap.Framework.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="shouldRemovePredicate"></param>
        /// <returns></returns>
        public static bool RemoveFirstWhere<T>(this IList<T> list, Func<T, bool> shouldRemovePredicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (shouldRemovePredicate.Invoke(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public static Random Random = new Random();
        public static T GetRandom<T>(this IList<T> list) => list[Random.Next(0, list.Count)];
    }
}
