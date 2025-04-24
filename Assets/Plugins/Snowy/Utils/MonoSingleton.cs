using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Snowy.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public virtual bool DestroyOnLoad => true;
        
        /// <summary>
        /// The singleton instance of a reference.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (Reference == null)
                {
                    if ((Reference = FindObjectOfType<T>()) == null)
                    {
                        throw new MissingReferenceException($"The singleton reference to a {typeof(T).Name} does not found!");
                    }
                }

                return Reference;
            }
        }

        public static bool HasReference
        {
            get
            {
                if (Reference == null)
                {
                    return (Reference = FindObjectOfType<T>()) != null;
                }

                return true;
            }
        }

        protected static T Reference;
        
        protected virtual void Awake()
        {
            if (!DestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            
            Reset();
        }

        protected void Reset()
        {
#if UNITY_EDITOR
            if (FindObjectsOfType<T>().Length > 1)
            {
                EditorUtility.DisplayDialog("Singleton Error", $"There should never be more than 1 reference of {typeof(T).Name}!", "OK");
                DestroyImmediate(this);
            }
#endif
        }
    }
}