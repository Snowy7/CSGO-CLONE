﻿#if !UNITY_2021_2_OR_NEWER
using System.Runtime.CompilerServices;
using Snowy.Mathematics;

namespace System
{
    /// <summary>
    /// Temporary solutions for .NET standard 2.0 and less
    /// </summary>
    internal static class MathF
    {
        public const float E = 2.71828183f;
        public const float PI = 3.14159265f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float value)
        {
            return (float)Math.Acos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float value)
        {
            return (float)Math.Asin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float value)
        {
            return (float)Math.Atan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cbrt(float value)
        {
            return (float)Math.Pow(value, MathUtility.THIRD);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float value)
        {
            return (float)Math.Ceiling(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cosh(float value)
        {
            return (float)Math.Cosh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float value)
        {
            return (float)Math.Exp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float value)
        {
            return (float)Math.Floor(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float IEEERemainder(float x, float y)
        {
            return (float)Math.IEEERemainder(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float value)
        {
            return (float)Math.Log(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float value, float newBase)
        {
            return (float)Math.Log(value, newBase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log10(float value)
        {
            return (float)Math.Log10(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b)
        {
            return Math.Max(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float a, float b)
        {
            return Math.Min(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float value, float pow)
        {
            return (float)Math.Pow(value, pow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float value, int decimals, MidpointRounding mode)
        {
            return (float)Math.Round(value, decimals, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float value, MidpointRounding mode)
        {
            return (float)Math.Round(value, mode);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(float value)
        {
            return (float)Math.Round(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sign(float value)
        {
            return Math.Sign(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sinh(float value)
        {
            return (float)Math.Sinh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tanh(float value)
        {
            return (float)Math.Tanh(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Truncate(float value)
        {
            return (float)Math.Truncate(value);
        }
    }
}
#endif
