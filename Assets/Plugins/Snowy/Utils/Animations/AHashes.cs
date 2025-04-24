

using UnityEngine;

namespace Core
{
    /// <summary>
    /// Animator Hashes.
    /// </summary>
    public static class AHashes
    {
        /// <summary>
        /// Hashed "Movement".
        /// </summary>
        public static readonly int Movement = Animator.StringToHash("Movement");
        
        /// <summary>
        /// Hashed "Horizontal".
        /// </summary>
        public static readonly int Horizontal = Animator.StringToHash("Horizontal");
        /// <summary>
        /// Hashed "Vertical".
        /// </summary>
        public static readonly int Vertical = Animator.StringToHash("Vertical");
    }
}