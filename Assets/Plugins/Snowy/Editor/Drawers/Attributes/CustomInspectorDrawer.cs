using System.Reflection;
using Toolbox.Editor;
using UnityEngine;
using UnityEditor;
using Utils.Attributes;

namespace Editors
{
    [CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
    public class CustomInspectorDrawer : ToolboxEditor
    {
        bool showInspectorHeader;
        InspectorHeaderAttribute headerAttribute;

        bool showInspectorHelp;
        HelpBoxAttribute helpAttribute;

        private void OnEnable()
        {
            headerAttribute = (InspectorHeaderAttribute)target.GetType().GetCustomAttribute(typeof(InspectorHeaderAttribute), false);
            helpAttribute = (HelpBoxAttribute)target.GetType().GetCustomAttribute(typeof(HelpBoxAttribute), false);

            showInspectorHeader = headerAttribute != null;
            showInspectorHelp = helpAttribute != null;
        }

        public override void DrawCustomInspector()
        {
            if (showInspectorHeader)
            {
                string title = headerAttribute.title;
                string icon = headerAttribute.icon;

                GUIContent titleGUI = new GUIContent(title);
                if (!string.IsNullOrEmpty(icon))
                    titleGUI = EditorGUIUtility.TrTextContentWithIcon(" " + title, icon);

                EditorDrawing.DrawInspectorHeader(titleGUI, target);
                if(headerAttribute.space) EditorGUILayout.Space();
            }

            if (showInspectorHelp)
            {
                EditorGUILayout.HelpBox(helpAttribute.message, (MessageType)helpAttribute.messageType);
            }

            IgnoreProperty("m_Script");
            base.DrawCustomInspector();
        }
    }
}