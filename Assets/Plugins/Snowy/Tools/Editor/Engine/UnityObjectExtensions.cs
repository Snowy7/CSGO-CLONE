﻿using System;
using System.Linq;
using Snowy.Engine;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace SnowyEditor.Engine
{
    public static class UnityObjectExtensions
    {
        public static void DestroyImmediate(this UnityObject self)
        {
            UnityObject.DestroyImmediate(self);
        }

        public static void DestroyChildrenImmediate(this Transform self)
        {
            Transform[] children = self.GetTopChildren();

            for (int i = 0; i < children.Length; i++)
            {
                children[i].gameObject.DestroyImmediate();
            }
        }

        public static void DestroyChildrenImmediate(this Transform self, Predicate<Transform> predicate)
        {
            Transform[] children = self.EnumerateChildren(false)
                                       .Where(item => predicate(item))
                                       .ToArray();

            for (int i = 0; i < children.Length; i++)
            {
                children[i].gameObject.DestroyImmediate();
            }
        }
    }
}
