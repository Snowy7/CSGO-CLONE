using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class TriggerCollider : MonoBehaviour
    {
        [SerializeField, TagSelector] private string tagToCheck = "Player";
        [SerializeField] UnityEvent onTriggerEnter;
        [SerializeField] UnityEvent onTriggerExit;
        
        private void OnTriggerEnter(Collider other)
        {
            // They are not equal
            if (!other.CompareTag(tagToCheck))
                return;
            
            onTriggerEnter.Invoke();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(tagToCheck))
                return;
            
            onTriggerExit.Invoke();
        }
    }
}