using System;
using UnityEngine;

namespace OMG.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// callback?.Invoke(digit(1, 10, 100), digit_number(523 => 3, 2, 5), index_total);
        /// </summary>
        public static void ForEachDigit(this int source, Action<int, int, int> callback)
        {
            int i = 0;
            int origin = source;
            int cursor = (int)MathF.Pow(10, origin.ToString().Length - 1);
            while (cursor > 0)
            {
                int number = origin / cursor;
                for (int j = 0; j < number; j++, i++)
                    callback?.Invoke(cursor, number, i);

                origin %= cursor;
                cursor /= 10;
            }
        }

        /// <summary>
        /// callback?.Invoke(digit(1, 10, 100), digit_number(523 => 3, 2, 5), index_total);
        /// </summary>
        public static void ForEachDigit(this ushort source, Action<ushort, ushort, int> callback)
        {
            int i = 0;
            ushort origin = source;
            ushort cursor = (ushort)MathF.Pow(10, origin.ToString().Length - 1);
            while (cursor > 0)
            {
                ushort number = (ushort)(origin / cursor);
                for (int j = 0; j < number; j++, i++)
                    callback?.Invoke(cursor, number, i);

                origin %= cursor;
                cursor /= 10;
            }
        }

        public static float ArithmeticSequence(float n, float start, float diff) => start + (n - 1) * diff;
    }
}