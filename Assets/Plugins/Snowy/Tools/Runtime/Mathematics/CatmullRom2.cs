using System;
using System.Collections.Generic;
using Snowy.CSharp;
using Snowy.Tools;
using UnityEngine;

namespace Snowy.Mathematics
{
    [Serializable]
    public class CatmullRom2 : ICurve<Vector2>
    {
        internal const int REQUIRED_COUNT = 4;

        private Vector2[] _points;
        private float _alpha = 0.5f;
        [NonSerialized]
        private float[] _weights;
        [NonSerialized]
        private bool _weightsInitialized;

        public bool PreciseWeights { get; set; }
        public int Count => _points.Length;

        public Vector2 this[int index]
        {
            get => _points[index];
            set
            {
                if (_points[index] == value)
                    return;

                _points[index] = value;
                _weightsInitialized = false;
            }
        }

        public float Alpha
        {
            get => _alpha;
            set
            {
                if (_alpha == value)
                    return;

                _alpha = value.Clamp01();
                _weightsInitialized = false;
            }
        }

        public CatmullRom2(Vector2[] points) : this((IList<Vector2>)points)
        {

        }

        public CatmullRom2(IList<Vector2> points)
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
                return _points[1];

            if (ratio >= 1f)
                return _points.FromEnd(1);

            if (!_weightsInitialized)
            {
                if (_weights == null)
                {
                    if (_points.Length > REQUIRED_COUNT)
                        _weights = new float[_points.Length - 3];
                    else
                        _weights = Array.Empty<float>();
                }

                RecalculateWeights(_points, _weights, _alpha, PreciseWeights);
                _weightsInitialized = true;
            }

            int weightIndex = 0;

            float sum = 0f;
            for (int i = 0; i < _weights.Length; i++)
            {
                if (ratio > sum + _weights[i])
                {
                    sum += _weights[i];
                    continue;
                }

                ratio = (ratio - sum) / _weights[i];
                weightIndex = i;
                break;
            }

            return EvaluateFourPoints(_points[weightIndex], _points[weightIndex + 1], _points[weightIndex + 2], _points[weightIndex + 3], _alpha, ratio);
        }

        public static Vector2 Evaluate(in Vector2 controlPoint1, in Vector2 point1, in Vector2 point2, in Vector2 controlPoint2, float alpha, float ratio)
        {
            if (ratio <= 0f)
                return point1;

            if (ratio >= 1f)
                return point2;

            return EvaluateFourPoints(controlPoint1, point1, point2, controlPoint2, alpha.Clamp01(), ratio);
        }

        private static Vector2 EvaluateFourPoints(Vector2 point0, Vector2 point1, Vector2 point2, Vector2 point3, float alpha, float ratio)
        {
            float pow = alpha * 0.5f;

            float t0 = 0f;
            float t1 = Vector2.SqrMagnitude(point0 - point1).Pow(pow);
            float t2 = Vector2.SqrMagnitude(point1 - point2).Pow(pow) + t1;
            float t3 = Vector2.SqrMagnitude(point2 - point3).Pow(pow) + t2;
            float t = Mathf.LerpUnclamped(t1, t2, ratio);

            point0 = Vector2.LerpUnclamped(point0, point1, (t - t0) / (t1 - t0));
            point1 = Vector2.LerpUnclamped(point1, point2, (t - t1) / (t2 - t1));
            point2 = Vector2.LerpUnclamped(point2, point3, (t - t2) / (t3 - t2));

            point0 = Vector2.LerpUnclamped(point0, point1, (t - t0) / (t2 - t0));
            point1 = Vector2.LerpUnclamped(point1, point2, (t - t1) / (t3 - t1));

            return Vector2.LerpUnclamped(point0, point1, (t - t1) / (t2 - t1));
        }

        private static void RecalculateWeights(Vector2[] points, float[] weights, float alpha, bool preciseWeights)
        {
            if (weights.Length == 0)
                return;

            float sum = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                Vector2 distant = preciseWeights ? EvaluateFourPoints(points[i], points[i + 1], points[i + 2], points[i + 3], alpha, 0.1f)
                                                 : points[i + 2];
                float distance = Vector2.Distance(points[i + 1], distant);
                weights[i] = distance;
                sum += distance;
            }

            float normalizeFactor = 1f / sum;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] *= normalizeFactor;
            }
        }
    }
}
