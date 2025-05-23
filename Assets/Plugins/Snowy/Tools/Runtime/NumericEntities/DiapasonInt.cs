﻿using System;
using Snowy.Tools;

namespace Snowy.NumericEntities
{
#if UNITY
    [Serializable]
#endif
    public struct DiapasonInt
    {
#if UNITY
        [UnityEngine.Serialization.FormerlySerializedAs("x")]
        [UnityEngine.Serialization.FormerlySerializedAs("From")]
#endif
        public int Min;
#if UNITY
        [UnityEngine.Serialization.FormerlySerializedAs("y")]
        [UnityEngine.Serialization.FormerlySerializedAs("Before")]
#endif
        public int Max;

#if UNITY_EDITOR
        internal static string MinFieldName = nameof(Min);
        internal static string MaxFieldName = nameof(Max);
#endif

        public DiapasonInt(int min, int max)
        {
            if (min > max)
                throw ThrowErrors.MinMax(nameof(min), nameof(max));

            Min = min;
            Max = max;
        }

        public void Deconstruct(out int from, out int before)
        {
            from = Min;
            before = Max;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        public static implicit operator (int min, int max)(DiapasonInt value)
        {
            return (value.Min, value.Max);
        }

        public static implicit operator DiapasonInt((int, int) value)
        {
            return new DiapasonInt(value.Item1, value.Item2);
        }

        public static explicit operator DiapasonInt(Diapason value)
        {
            return new DiapasonInt((int)value.Min, (int)value.Max);
        }
    }
}
