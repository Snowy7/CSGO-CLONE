﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Snowy.Mathematics
{
    public enum RoundingWay
    {
        ToNearest,
        Ceiling,
        Floor,
    }

    public static class MathExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity.
        /// </summary>
        public static bool IsInfinity(this float value)
        {
            return float.IsInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        public static bool IsNegInfinity(this float value)
        {
            return float.IsNegativeInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        public static bool IsPosInfinity(this float value)
        {
            return float.IsPositiveInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to not a number.
        /// </summary>
        public static bool IsNaN(this float value)
        {
            return float.IsNaN(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity.
        /// </summary>
        public static bool IsInfinity(this double value)
        {
            return double.IsInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        public static bool IsNegInfinity(this double value)
        {
            return double.IsNegativeInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        public static bool IsPosInfinity(this double value)
        {
            return double.IsPositiveInfinity(value);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to not a number.
        /// </summary>
        public static bool IsNaN(this double value)
        {
            return double.IsNaN(value);
        }

#if UNITY
        /// <summary>
        /// Compares two big floating point values if they are similar.
        /// </summary>
        public static bool Approx(this float value, float other)
        {
            return UnityEngine.Mathf.Approximately(value, other);
        }
#endif

        public static int DigitsCount(this int number)
        {
            return MathUtility.DigitsCount(number);
        }

        public static int DigitsCount(this long number)
        {
            return MathUtility.DigitsCount(number);
        }

        public static int[] GetDigits(this int number)
        {
            List<int> buffer = new List<int>();
            MathUtility.GetDigits(number, buffer);
            return buffer.ToArray();
        }

        public static int[] GetDigits(this long number)
        {
            List<int> buffer = new List<int>();
            MathUtility.GetDigits(number, buffer);
            return buffer.ToArray();
        }

        public static void GetDigits(this int number, List<int> buffer)
        {
            MathUtility.GetDigits(number, buffer);
        }

        public static void GetDigits(this long number, List<int> buffer)
        {
            MathUtility.GetDigits(number, buffer);
        }

        /// <summary>
        /// Returns the sign of value (1, -1 or 0).
        /// </summary>
        public static int Sign0(this float value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns the sign of value (1, -1 or 0).
        /// </summary>
        public static int Sign0(this double value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns the sign of value (1, -1 or 0).
        /// </summary>
        public static int Sign0(this int value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns the sign of value (1, -1 or 0).
        /// </summary>
        public static int Sign0(this long value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns the sign of value (1 of -1).
        /// </summary>
        public static int Sign(this float value)
        {
            return value >= 0 ? 1 : -1;
        }

        /// <summary>
        /// Returns the sign of value (1 of -1).
        /// </summary>
        public static int Sign(this double value)
        {
            return value >= 0 ? 1 : -1;
        }

        /// <summary>
        /// Returns the sign of value (1 of -1).
        /// </summary>
        public static int Sign(this int value)
        {
            return value >= 0 ? 1 : -1;
        }

        /// <summary>
        /// Returns the sign of value (1 of -1).
        /// </summary>
        public static int Sign(this long value)
        {
            return value >= 0 ? 1 : -1;
        }

        /// <summary>
        /// Returns true if the value is in range between min [inclusive] and max [inclusive].
        /// </summary>
        public static bool IsInBounds(this float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Returns true if the value is in range between min [inclusive] and max [inclusive].
        /// </summary>
        public static bool IsInBounds(this double value, double min, double max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Returns true if the value is in range between min [inclusive] and max [exclusive].
        /// </summary>
        public static bool IsInBounds(this int value, int min, int max)
        {
            return value >= min && value < max;
        }

        /// <summary>
        /// Returns true if the value is in range between min [inclusive] and max [exclusive].
        /// </summary>
        public static bool IsInBounds(this long value, long min, long max)
        {
            return value >= min && value < max;
        }

        /// <summary>
        /// Rounds the float value to the nearest integer.
        /// </summary>
        public static float Round(this float value)
        {
            return MathF.Round(value);
        }

        /// <summary>
        /// Rounds the float value to the nearest integer.
        /// </summary>
        public static double Round(this double value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// Rounds the float value to the nearest value multiple to the specified step.
        /// </summary>
        public static float Round(this float value, float snapStep)
        {
            if (snapStep <= 0f)
                return value;

            return Round(value / snapStep) * snapStep;
        }

        /// <summary>
        /// Rounds the float value to the nearest value multiple to the specified step.
        /// </summary>
        public static double Round(this double value, double snapStep)
        {
            if (snapStep <= 0f)
                return value;

            return Round(value / snapStep) * snapStep;
        }

        /// <summary>
        /// Returns the smallest integer greater to or equal to the value.
        /// </summary>
        public static float Ceiling(this float value)
        {
            return MathF.Ceiling(value);
        }

        /// <summary>
        /// Returns the smallest integer greater to or equal to the value.
        /// </summary>
        public static double Ceiling(this double value)
        {
            return Math.Ceiling(value);
        }

        /// <summary>
        /// Returns the largest integer smaller to or equal to the value.
        /// </summary>
        public static float Floor(this float value)
        {
            return MathF.Floor(value);
        }

        /// <summary>
        /// Returns the largest integer smaller to or equal to the value.
        /// </summary>
        public static double Floor(this double value)
        {
            return Math.Floor(value);
        }

        /// <summary>
        /// Returns the whole part of the specified value.
        /// </summary>
        public static float Truncate(this float value)
        {
            return MathF.Truncate(value);
        }

        /// <summary>
        /// Returns the whole part of the specified value.
        /// </summary>
        public static double Truncate(this double value)
        {
            return Math.Truncate(value);
        }

        /// <summary>
        /// Returns the decimal part of the specified value.
        /// </summary>
        public static float Decim(this float value)
        {
            return value % 1;
        }

        /// <summary>
        /// Returns the decimal part of the specified value.
        /// </summary>
        public static double Decim(this double value)
        {
            return value % 1;
        }

        /// <summary>
        /// Returns the absolute value of the number.
        /// </summary>
        public static float Abs(this float value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of the number.
        /// </summary>
        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of the number.
        /// </summary>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of the number.
        /// </summary>
        public static long Abs(this long value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Raises the value to the specified power.
        /// </summary>
        public static float Pow(this float value, float pow)
        {
            return MathF.Pow(value, pow);
        }

        /// <summary>
        /// Raises the value to the specified power.
        /// </summary>
        public static double Pow(this double value, double pow)
        {
            return Math.Pow(value, pow);
        }

        /// <summary>
        /// Raises the value to the specified power.
        /// </summary>
        public static float Pow(this int value, float pow)
        {
            return MathF.Pow(value, pow);
        }

        /// <summary>
        /// Raises the value to the specified power.
        /// </summary>
        public static double Pow(this long value, double pow)
        {
            return Math.Pow(value, pow);
        }

        /// <summary>
        /// Returns square root of the value.
        /// </summary>
        public static float Sqrt(this float value)
        {
            return MathF.Sqrt(value);
        }

        /// <summary>
        /// Returns square root of the value.
        /// </summary>
        public static double Sqrt(this double value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// Returns cubic root of the value.
        /// </summary>
        public static float Cbrt(this float value)
        {
            return MathF.Cbrt(value);
        }

        /// <summary>
        /// Returns cubic root of the value.
        /// </summary>
        public static double Cbrt(this double value)
        {
#if UNITY_2021_2_OR_NEWER || !UNITY
            return Math.Cbrt(value);
#else
            return Math.Pow(value, MathUtility.THIRD);
#endif
        }

        /// <summary>
        /// Returns square root of the value.
        /// </summary>
        public static float Sqrt(this int value)
        {
            return MathF.Sqrt(value);
        }

        /// <summary>
        /// Returns square root of the value.
        /// </summary>
        public static double Sqrt(this long value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// Returns cubic root of the value.
        /// </summary>
        public static float Cbrt(this int value)
        {
            return MathF.Cbrt(value);
        }

        /// <summary>
        /// Returns cubic root of the value.
        /// </summary>
        public static double Cbrt(this long value)
        {
#if UNITY_2021_2_OR_NEWER || !UNITY
            return Math.Cbrt(value);
#else
            return Math.Pow(value, MathUtility.THIRD);
#endif
        }

        /// <summary>
        /// Calculates the linear parameter that produces the interpolant value within the range [a, b] (InverseLerp).
        /// </summary>
        public static float RatioToValue(this float value, float a, float b)
        {
            if (a == b)
                return 0f;

            return Clamp01((value - a) / (b - a));
        }

        /// <summary>
        /// Calculates the linear parameter that produces the interpolant value within the range [a, b] (InverseLerp).
        /// </summary>
        public static double RatioToValue(this double value, double a, double b)
        {
            return a == b ? 0d : Clamp01((value - a) / (b - a));
        }

        /// <summary>
        /// Calculates the linear parameter that produces the interpolant value within the range [a, b] (InverseLerp).
        /// </summary>
        public static float RatioToValue(this int value, int a, int b)
        {
            if (a == b)
                return 0f;

            return Clamp01((value - a) / (b - a));
        }

        /// <summary>
        /// Calculates the linear parameter that produces the interpolant value within the range [a, b] (InverseLerp).
        /// </summary>
        public static double RatioToValue(this long value, long a, long b)
        {
            return RatioToValue((double)value, a, b);
        }

        /// <summary>
        /// Clamps the value between a minimum float and maximum float value.
        /// </summary>
        public static float Clamp(this float value, float min, float max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Clamps the value between a minimum float and maximum float value.
        /// </summary>
        public static double Clamp(this double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Clamps the value between a minimum float and maximum float value.
        /// </summary>
        public static float Clamp(this float value, in (float min, float max) range)
        {
            return Clamp(value, range.min, range.max);
        }

        /// <summary>
        /// Clamps the value between a minimum float and maximum float value.
        /// </summary>
        public static double Clamp(this double value, in (double min, double max) range)
        {
            return Clamp(value, range.min, range.max);
        }

        /// <summary>
        /// Clamps the value between the specified minimum float value and float.PositiveInfinity.
        /// </summary>
        public static float ClampMin(this float value, float min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// Clamps the value between the specified minimum float value and float.PositiveInfinity.
        /// </summary>
        public static double ClampMin(this double value, double min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// Clamps the value between float.NegativeInfinity and the specified maximum float value.
        /// </summary>
        public static float ClampMax(this float value, float max)
        {
            return value > max ? max : value;
        }

        /// <summary>
        /// Clamps the value between float.NegativeInfinity and the specified maximum float value.
        /// </summary>
        public static double ClampMax(this double value, double max)
        {
            return value > max ? max : value;
        }

        /// <summary>        
        /// Clamps the value between 0 and 1.
        /// </summary>
        public static float Clamp01(this float value)
        {
            if (value < 0f)
                return 0f;

            if (value > 1f)
                return 1f;

            return value;
        }

        /// <summary>        
        /// Clamps the value between 0 and 1.
        /// </summary>
        public static double Clamp01(this double value)
        {
            return Clamp(value, 0, 1);
        }

        /// <summary>
        /// Clamps the value between a minimum int and maximum int value.
        /// </summary>
        public static int Clamp(this int value, int min, int max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Clamps the value between a minimum int and maximum int value.
        /// </summary>
        public static long Clamp(this long value, long min, long max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// Clamps the value between the specified minimum int value and int.MaxValue.
        /// </summary>
        public static int ClampMin(this int value, int min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// Clamps the value between the specified minimum int value and int.MaxValue.
        /// </summary>
        public static long ClampMin(this long value, long min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// Clamps the value between int.MinValue and the specified maximum int value.
        /// </summary>
        public static int ClampMax(this int value, int max)
        {
            return value > max ? max : value;
        }

        /// <summary>
        /// Clamps the value between int.MinValue and the specified maximum int value.
        /// </summary>
        public static long ClampMax(this long value, long max)
        {
            return value > max ? max : value;
        }

        /// <summary>
        /// Loops the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static float Repeat(this float value, float length)
        {
            return Clamp(value - Floor(value / length) * length, 0f, length);
        }

        /// <summary>
        /// Loops the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static double Repeat(this double value, double length)
        {
            return Clamp(value - Math.Floor(value / length) * length, 0, length);
        }

        /// <summary>
        /// Loops the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static int Repeat(this int value, int length)
        {
            int res = value % length;
            return res >= 0 ? res : res + length;
        }

        /// <summary>
        /// Loops the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static long Repeat(this long value, long length)
        {
            long res = value % length;
            return res >= 0 ? res : res + length;
        }

        /// <summary>
        /// PingPongs the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static float PingPong(this float value, float length)
        {
            value = Repeat(value, length * 2f);
            return length - Abs(value - length);
        }

        /// <summary>
        /// PingPongs the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static double PingPong(this double value, float length)
        {
            value = Repeat(value, length * 2);
            return length - Math.Abs(value - length);
        }

        /// <summary>
        /// PingPongs the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static int PingPong(this int value, int length)
        {
            value = Repeat(value, length * 2);
            return length - Math.Abs(value - length);
        }

        /// <summary>
        /// PingPongs the value, so that it is never larger than length and never smaller than 0.
        /// </summary>
        public static long PingPong(this long value, int length)
        {
            value = Repeat(value, length * 2);
            return length - Math.Abs(value - length);
        }

        /// <summary>
        /// Transfers the value from radians to degrees.
        /// </summary>
        public static float ToDegrees(this float value)
        {
            return value * MathUtility.Rad2Deg;
        }

        /// <summary>
        /// Transfers the value from radians to degrees.
        /// </summary>
        public static double ToDegrees(this double value)
        {
            return value * MathUtility.Rad2Deg;
        }

        /// <summary>
        /// Transfers the value from degrees to radians.
        /// </summary>
        public static float ToRadians(this float value)
        {
            return value * MathUtility.Deg2Rad;
        }

        /// <summary>
        /// Transfers the value from degrees to radians.
        /// </summary>
        public static double ToRadians(this double value)
        {
            return value * MathUtility.Deg2Rad;
        }

#if UNITY
        /// <summary>
        /// Returns true if the value is power of two.
        /// </summary>
        public static bool IsPoT(this int value)
        {
            return UnityEngine.Mathf.IsPowerOfTwo(value);
        }

        /// <summary>
        /// Returns the closest power of two value.
        /// </summary>
        public static int ToClosestPoT(this int value)
        {
            return UnityEngine.Mathf.ClosestPowerOfTwo(value);
        }
#endif

        /// <summary>
        /// Converts the boolean value to integer.
        /// </summary>
        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }

        /// <summary>
        /// Casts float value to integer.
        /// </summary>
        public static int ToInt(this float value, RoundingWay rounding)
        {
            switch (rounding)
            {
                case RoundingWay.ToNearest:
                    return (int)value.Round();

                case RoundingWay.Ceiling:
                    return (int)MathF.Ceiling(value);

                case RoundingWay.Floor:
                    return (int)MathF.Floor(value);

                default:
                    throw new SwitchExpressionException(rounding);
            }
        }

        /// <summary>
        /// Casts double value to long.
        /// </summary>
        public static long ToLong(this double value, RoundingWay rounding)
        {
            switch (rounding)
            {
                case RoundingWay.ToNearest:
                    return (int)Round(value);

                case RoundingWay.Ceiling:
                    return (int)Math.Ceiling(value);

                case RoundingWay.Floor:
                    return (int)Math.Floor(value);

                default:
                    throw new SwitchExpressionException(rounding);
            }
        }

        /// <summary>
        /// Casts integer value to float.
        /// </summary>
        public static float ToFloat(this int value)
        {
            return value;
        }

        /// <summary>
        /// Casts long value to double.
        /// </summary>
        public static double ToDouble(this long value)
        {
            return value;
        }
    }
}
