using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace OMG.Extensions
{
    public static class CollectionExtensions
    {
        public static T PickRandom<T>(this T[] source)
        {
            int randomIndex = Random.Range(0, source.Length);
            if (randomIndex >= source.Length)
                return default(T);

            return source[randomIndex];
        }

        public static T PickRandom<T>(this List<T> source)
        {
            int randomIndex = Random.Range(0, source.Count);
            if (randomIndex >= source.Count)
                return default(T);

            return source[randomIndex];
        }

        public static Dictionary<Type, T> GetTypeDictionary<T>(this T[] source)
        {
            Dictionary<Type, T> dictionary = new Dictionary<Type, T>();
            for (int i = 0; i < source.Length; ++i)
            {
                Type type = source[i].GetType();
                if (dictionary.ContainsKey(type))
                    continue;

                dictionary.Add(type, source[i]);
            }

            return dictionary;
        }

        public static void ForEach<T>(this T[] source, Action<T> callback)
        {
            for (int i = 0; i < source.Length; ++i)
                callback?.Invoke(source[i]);
        }

        public static void ForEach<T>(this T[] source, Action<T, int> callback)
        {
            for (int i = 0; i < source.Length; ++i)
                callback?.Invoke(source[i], i);
        }
    }
}