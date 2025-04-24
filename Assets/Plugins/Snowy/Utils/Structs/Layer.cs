using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct Layer
    {
        public int index;

        public static implicit operator int(Layer layer)
        {
            return layer.index;
        }

        public static implicit operator Layer(int intVal)
        {
            Layer result = default;
            result.index = intVal;
            return result;
        }

        public bool CompareLayer(GameObject obj)
        {
            return obj.layer == this;
        }
    }
}