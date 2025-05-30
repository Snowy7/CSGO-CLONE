﻿#if INCLUDE_PHYSICS || INCLUDE_PHYSICS_2D
using System;
using Snowy.Mathematics;
using UnityEngine;

namespace Snowy.Shooting
{
    [Serializable]
    internal struct DragOptions
    {
        public DragMethod Method;
        [Min(0f)]
        public float Value;
    }

    public enum DragMethod
    {
        None = 0,
        Linear = 1,
        NonLinear = 2,
    }

    [Serializable]
    public struct RicochetOptions
    {
        [SerializeField]
        private int _count;
        [SerializeField]
        private LayerMask _ricochetMask;
        [SerializeField, Range(0f, 1f)]
        private float _speedLoss;

        private int _ricochetsLeft;

#if UNITY_EDITOR
        public static string CountFieldName => nameof(_count);
        public static string MaskFieldName => nameof(_ricochetMask);
        public static string LossFieldName => nameof(_speedLoss);
#endif

        public LayerMask RicochetMask
        {
            get => _ricochetMask;
            set => _ricochetMask = value;
        }

        public int Count
        {
            get => _count;
            set => _count = value.ClampMin(0);
        }

        public float SpeedLoss
        {
            get => _speedLoss;
            set => _speedLoss = value.Clamp01();
        }

        internal float SpeedRemainder => 1f - _speedLoss;
        internal int RicochetsLeft => _ricochetsLeft;

        public RicochetOptions(LayerMask mask, int count, float speedLoss)
        {
            _ricochetMask = mask;
            _count = count.ClampMin(0);
            _speedLoss = speedLoss.Clamp01();
            _ricochetsLeft = 0;
        }

        internal void ResetRicochets()
        {
            _ricochetsLeft = Count;
        }

        internal void DecreaseCounter()
        {
            _ricochetsLeft--;
        }
    }

    [Serializable]
    internal struct CastOptions
    {
        [Min(0f)]
        public float CastRadius;
        public bool HighPrecision;
    }
}
#endif
