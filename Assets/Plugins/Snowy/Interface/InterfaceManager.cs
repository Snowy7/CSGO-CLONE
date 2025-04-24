using Snowy.Utils;
using UnityEngine;

namespace Interface
{
    public class InterfaceManager : MonoSingleton<InterfaceManager>
    {
        [Header("References")]
        [SerializeField] Canvas canvas;
        [SerializeField, ReorderableList] Element[] elements;
        
        [Header("Setup")]
        [SerializeField] float distance = 5f;
        
        private void Update()
        {
            foreach (var element in elements)
            {
                if (element.enabled) element.Tick();
            }
        }
    }
}