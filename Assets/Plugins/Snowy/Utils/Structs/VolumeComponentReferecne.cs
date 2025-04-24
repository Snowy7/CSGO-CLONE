using System;
using UnityEngine.Rendering;

namespace Utils
{
    [Serializable]
    public struct VolumeComponentReferecne
    {
        public Volume Volume;
        public int ComponentIndex;

        public VolumeComponent GetVolumeComponent()
        {
            return Volume.profile.components[ComponentIndex];
        }
    }
}