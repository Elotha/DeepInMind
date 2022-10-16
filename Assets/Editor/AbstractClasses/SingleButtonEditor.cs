using System;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Editor.AbstractClasses
{
    public abstract class SingleButtonEditor : UnityEditor.Editor
    {
        protected abstract string ButtonName { get; }
        protected abstract Action Function { get; }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button(ButtonName))
            {
                Function.Invoke();
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}