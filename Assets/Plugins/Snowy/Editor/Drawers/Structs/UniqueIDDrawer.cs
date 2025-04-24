using UnityEngine;
using UnityEditor;
using Utils;

namespace Editors
{
    [CustomPropertyDrawer(typeof(UniqueID))]
    public class UniqueIDDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) { }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0f;
    }
}