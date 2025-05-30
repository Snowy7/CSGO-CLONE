﻿using System;
using Snowy;
using Snowy.CSharp;
using Snowy.CSharp.Collections;
using Snowy.Inspector;
using SnowyEditor.Window;
using UnityEditor;
using UnityEngine;

namespace SnowyEditor.Drawers.Attributes
{
    //Based on https://forum.unity.com/threads/serializereference-genericserializedreferenceinspectorui.813366/
    [CustomPropertyDrawer(typeof(ReferenceSelectionAttribute))]
    internal class ReferenceSelectionDrawer : SerializeReferenceDrawer
    {
        protected override void DrawExtendedContent(in Rect position, SerializedProperty property)
        {
            DrawContent(position, property);
        }

        public static void DrawContent(in Rect position, SerializedProperty property, params Type[] rootTypes)
        {
            Type assignedType = EditorUtilityExt.GetTypeFromSerializedPropertyTypename(property.managedReferenceFullTypename);
            bool nullRef = assignedType == null;
            string label = nullRef ? "Null" : assignedType.Name;

            if (nullRef)
                GUI.color = Colours.Orange;

            if (EditorGUI.DropdownButton(position, EditorGuiUtility.TempContent(label), FocusType.Keyboard))
            {
                Type fieldType = EditorUtilityExt.GetTypeFromSerializedPropertyTypename(property.managedReferenceFieldTypename);

                if (fieldType == null)
                {
                    Debug.LogError("Can not get type from type name.");
                    return;
                }

                DropDownWindow menu = ScriptableObject.CreateInstance<DropDownWindow>();

                menu.AddItem("Null", assignedType == null, () => assignField(property, null));

                if (rootTypes.IsNullOrEmpty())
                {
                    addMenuItem(fieldType);

                    TypeCache.GetTypesDerivedFrom(fieldType)
                             .ForEach(type => addMenuItem(type));
                }
                else
                {
                    foreach (Type rootType in rootTypes)
                    {
                        if (!rootType.IsAssignableTo(fieldType))
                        {
                            Debug.LogWarning($"{rootType.Name} is not subclass of {fieldType.Name}");
                            continue;
                        }

                        addMenuItem(rootType);

                        TypeCache.GetTypesDerivedFrom(rootType)
                                 .ForEach(type => addMenuItem(type));
                    }
                }

                menu.ShowMenu(position);

                void addMenuItem(Type type)
                {
                    if (!type.IsAbstract && !type.IsInterface)
                    {
                        string entryName = $"{type.Name}  ({type.Namespace})";
                        menu.AddItem(entryName, type == assignedType, () => assignField(property, Activator.CreateInstance(type)));
                    }
                }

                void assignField(SerializedProperty prop, object newValue)
                {
                    prop.serializedObject.Update();
                    prop.managedReferenceValue = newValue;
                    prop.isExpanded = false;
                    prop.serializedObject.ApplyModifiedProperties();
                }
            }

            GUI.color = Colours.White;
        }
    }
}
