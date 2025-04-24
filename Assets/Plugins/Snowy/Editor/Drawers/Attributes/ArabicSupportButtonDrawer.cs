using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Attributes;

namespace Editors
{
    [CustomPropertyDrawer(typeof(ArabicSupportButtonAttribute))]
    public class ArabicSupportButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            // Only works on text fields or text area fields
            if (prop.propertyType == SerializedPropertyType.String)
            {
                // if text area vertical layout 
                // if normal text field horizontal layout
                if ((attribute as ArabicSupportButtonAttribute).IsTextArea)
                {
                    // if is in array make sure there is a mnimum height
                    if (prop.isArray && prop.arraySize == 0)
                    {
                        prop.arraySize = 1;
                    }
                    
                    // Draw a text area using the default inspector
                    EditorGUI.BeginProperty(position, label, prop);
                    {
                        var buttonWidth = 20;
                        // height of the text area
                        // get the lines of the text
                        var lines = prop.stringValue.Split('\n');
                        // if the lines are more than 5 return the height of the lines
                        int height = Mathf.Min(5, lines.Length);
                        var textRect = new Rect(position.x, position.y, position.width - buttonWidth, EditorGUIUtility.singleLineHeight * height);
                        var buttonRect = new Rect(position.x + position.width - (buttonWidth * 3), position.y, (buttonWidth * 3), EditorGUIUtility.singleLineHeight * height);
                        
                        // Draw the property field (flexible height)
                        prop.stringValue = EditorGUI.TextArea(textRect, prop.stringValue);
                        
                        // Draw the button
                        if (GUI.Button(buttonRect, new GUIContent("A", "Click to fix Arabic text (Click more than once will miss up the text)")))
                        {
                            // Set the text to be right-to-left
                            prop.stringValue = ArabicSupport.ArabicFixer.Fix(prop.stringValue);
                            
                            // Refresh the editor
                            EditorUtility.SetDirty(prop.serializedObject.targetObject);
                            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                        }
                         
                        // Draw a second button that fixes the text in the opposite direction
                        buttonRect.x -= buttonWidth * 2;
                        buttonRect.width = buttonWidth * 2;

                        if (GUI.Button(buttonRect,
                                new GUIContent("AT",
                                    "Click to fix Arabic text (Click more than once will miss up the text)")))
                        {
                            // Set the text to be right-to-left
                            prop.stringValue = ArabicSupport.ArabicFixer.Fix(prop.stringValue, false);

                            // Refresh the editor
                            EditorUtility.SetDirty(prop.serializedObject.targetObject);
                            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                        }
                    }
                }
                else
                {
                    // Horizontal layout
                    EditorGUI.BeginProperty(position, label, prop);
                    {
                        // Horizontal layout
                        var buttonWidth = 20;
                        var buttonRect = new Rect(position.x + position.width - buttonWidth, position.y, buttonWidth, position.height);
                        var textRect = new Rect(position.x, position.y, position.width - (buttonWidth * 3), position.height);
                        // Draw the property field
                        EditorGUI.PropertyField(textRect, prop, label);
                        // Draw the button
                        if (GUI.Button(buttonRect, new GUIContent("A", "Click to fix Arabic text (Click more than once will miss up the text)")))
                        {
                            // Set the text to be right-to-left
                            prop.stringValue = ArabicSupport.ArabicFixer.Fix(prop.stringValue);
                            
                            // Refresh the editor
                            EditorUtility.SetDirty(prop.serializedObject.targetObject);
                            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                        }
                        
                        // Draw a second button that fixes the text in the opposite direction
                        buttonRect.x -= buttonWidth * 2;
                        buttonRect.width = buttonWidth * 2;
                        if (GUI.Button(buttonRect, new GUIContent("AT", "Click to fix Arabic text (Click more than once will miss up the text)")))
                        {
                            // Set the text to be right-to-left
                            prop.stringValue = ArabicSupport.ArabicFixer.Fix(prop.stringValue, false);
                            
                            // Refresh the editor
                            EditorUtility.SetDirty(prop.serializedObject.targetObject);
                            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                        }
                    }
                }
            }
            else
            {
                // Show a warning that this only works on text fields
                var warning = "ArabicSupportButton only works on string fields";
                var warningSize = GUI.skin.label.CalcSize(new GUIContent(warning));
                EditorGUI.LabelField(new Rect(position.x, position.y, position.width, position.height), warning);
                position.y += warningSize.y;
                // Draw the property
                EditorGUI.PropertyField(position, prop, label);
                return;
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            // If the text area is enabled, return the height of 5 lines
            if ((attribute as ArabicSupportButtonAttribute).IsTextArea)
            {
                // get the lines of the text
                var lines = prop.stringValue.Split('\n');
                // if the lines are more than 5 return the height of the lines
                int height = Mathf.Min(5, lines.Length);
                return EditorGUIUtility.singleLineHeight * height;
            }

            // Otherwise return the default height
            return base.GetPropertyHeight(prop, label);
        }
    }
}