﻿using System;
using UnityEngine;

namespace Snowy.Inspector
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DrawFlagsAttribute : PropertyAttribute
    {
        internal Type EnumType { get; }

        public DrawFlagsAttribute(Type enumType)
        {
            EnumType = enumType;
        }
    }
}
