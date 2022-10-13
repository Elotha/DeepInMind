using EraSoren._InputSystem;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Editor.InputSystem
{
    [CustomPropertyDrawer(typeof(InputSet))]
    public class InputSetDrawer : PropertyDrawer
    {
        public const string PrimaryKeyFieldName = "primaryKey";
        public const string SecondaryKeyFieldName = "secondaryKey";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var primaryKeyProperty = property.FindPropertyRelative(PrimaryKeyFieldName);
            var secondaryKeyProperty = property.FindPropertyRelative(SecondaryKeyFieldName);
            
            position = EditorGUI.PrefixLabel(position, label);

            
            var segmentWidth = position.width / 2f;
            position.width = segmentWidth;
            EditorGUI.PropertyField(position, primaryKeyProperty, GUIContent.none);
            
            position.x += segmentWidth;
            EditorGUI.PropertyField(position, secondaryKeyProperty, GUIContent.none);
        }
    }
}