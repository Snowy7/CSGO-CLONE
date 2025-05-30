﻿using System;
using System.Collections.Generic;
using Snowy.CSharp;
using Snowy.CSharp.Collections;
using Snowy.Tools;
using UnityEngine;

namespace Snowy.Mathematics
{
    [Serializable]
    public class Bezier2 : ICurve<Vector2>
    {
        internal const int REQUIRED_COUNT = 3;

        private Vector2[] _points;
        [NonSerialized]
        private Vector2[] _buffer;

        public int Count => _points.Length;

        public Vector2 this[int index]
        {
            get => _points[index];
            set => _points[index] = value;
        }

        public Bezier2(Vector2[] points) : this((IList<Vector2>)points)
        {

        }

        public Bezier2(IList<Vector2> points)
        {
            if (points == null)
                throw ThrowErrors.NullParameter(nameof(points));

            if (points.Count < REQUIRED_COUNT)
                throw ThrowErrors.InvalidCurvePoints(nameof(points), REQUIRED_COUNT);

            _points = new Vector2[points.Count];
            points.CopyTo(_points, 0);
        }

        public Vector2 Evaluate(float ratio)
        {
            if (ratio <= 0f)
                return _points[0];

            if (ratio >= 1f)
                return _points.FromEnd(0);

            if (_buffer == null)
                _buffer = new Vector2[_points.Length];

            _points.CopyTo(_buffer, 0);
            return EvaluateInternal(_buffer, ratio);
        }

        public static Vector2 Evaluate(in Vector2 origin, in Vector2 destination, in Vector2 controlPoint, float ratio)
        {
            if (ratio <= 0f)
                return origin;

            if (ratio >= 1f)
                return destination;

            Vector2 p1 = Vector2.LerpUnclamped(origin, controlPoint, ratio);
            Vector2 p2 = Vector2.LerpUnclamped(controlPoint, destination, ratio);
            return Vector2.LerpUnclamped(p1, p2, ratio);
        }

        public static Vector2 Evaluate(Vector2[] points, float ratio)
        {
#if UNITY_2021_2_OR_NEWER || !UNITY
            return Evaluate((Span<Vector2>)points, ratio);
#else
            return Evaluate((IList<Vector2>)points, ratio);
#endif
        }

        public static Vector2 Evaluate(Span<Vector2> points, float ratio)
        {
            if (points.Length < REQUIRED_COUNT)
                throw ThrowErrors.InvalidCurvePoints(nameof(points), REQUIRED_COUNT);

            if (ratio <= 0f)
                return points[0];

            if (ratio >= 1f)
                return points.FromEnd(0);

            Span<Vector2> tmp = stackalloc Vector2[points.Length];
            points.CopyTo(tmp);
            return EvaluateInternal(tmp, ratio);
        }

        public static Vector2 Evaluate(IList<Vector2> points, float ratio)
        {
            if (points == null)
                throw ThrowErrors.NullParameter(nameof(points));

            if (points.Count < REQUIRED_COUNT)
                throw ThrowErrors.InvalidCurvePoints(nameof(points), REQUIRED_COUNT);

            if (ratio <= 0f)
                return points[0];

            if (ratio >= 1f)
                return points.FromEnd(0);

            Span<Vector2> tmp = stackalloc Vector2[points.Count];
            points.CopyTo(tmp);
            return EvaluateInternal(tmp, ratio);
        }

        private static Vector2 EvaluateInternal(Span<Vector2> tmp, float ratio)
        {
            int counter = tmp.Length - 1;
            int times = counter;

            for (int i = 0; i < times; i++)
            {
                for (int j = 0; j < counter; j++)
                {
                    tmp[j] = Vector2.LerpUnclamped(tmp[j], tmp[j + 1], ratio);
                }

                counter--;
            }

            return tmp[0];
        }

        private static Vector2 EvaluateInternal(Vector2[] tmp, float ratio)
        {
            int counter = tmp.Length - 1;
            int times = counter;

            for (int i = 0; i < times; i++)
            {
                for (int j = 0; j < counter; j++)
                {
                    tmp[j] = Vector2.LerpUnclamped(tmp[j], tmp[j + 1], ratio);
                }

                counter--;
            }

            return tmp[0];
        }
    }
}
