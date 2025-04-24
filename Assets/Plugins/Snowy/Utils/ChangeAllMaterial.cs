using UnityEngine;

namespace Utils
{
    public class ChangeAllMaterial : MonoBehaviour
    {
        [SerializeField, ReorderableList] Renderer[] renderers;
        [SerializeField] Material material;
        
        public void ChangeMaterial()
        {
            foreach (var rend in renderers)
            {
                rend.sharedMaterial = material;
            }
        }
    }
}