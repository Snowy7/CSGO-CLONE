using System;
using UnityEngine;

namespace Utils.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class ArabicSupportButtonAttribute : PropertyAttribute
    {
        public bool IsTextArea { get; set; }
        public ArabicSupportButtonAttribute(bool isTextArea = false)
        {
            IsTextArea = isTextArea;
        }
    }
}