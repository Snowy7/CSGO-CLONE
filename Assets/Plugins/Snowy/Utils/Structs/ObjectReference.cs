using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public sealed class ObjectReference
    {
        public string GUID;
        public GameObject Object;
    }
}