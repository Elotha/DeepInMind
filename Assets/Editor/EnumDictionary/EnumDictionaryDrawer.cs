using EraSoren._Core.Helpers;
using UnityEditor;
using UnityEngine;

namespace EraSoren.Editor.EnumDictionary
{
    [CustomPropertyDrawer(typeof(EnumDictionaryBase), true)]
    public class EnumDictionaryDrawer : PropertyDrawer
    {
        private const string ValuesFieldName = "Values";
        private const string EnumTypeFieldName = "EnumType";

        private bool _isFoldout = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueArrayProperty = property.FindPropertyRelative(ValuesFieldName);
            var enumNames = property.FindPropertyRelative(EnumTypeFieldName).enumDisplayNames;

            position.height = EditorGUIUtility.singleLineHeight;
            _isFoldout = EditorGUI.Foldout(position,_isFoldout,label);
            position.y += EditorGUIUtility.singleLineHeight;

            if (_isFoldout)
            {
                EditorGUI.indentLevel++;
                DrawElements(ref position, valueArrayProperty, enumNames);
                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUIUtility.singleLineHeight;

            if (_isFoldout)
            {
                var arrayProperty = property.FindPropertyRelative(ValuesFieldName);
                var arraySize = arrayProperty.arraySize;
                for (int i = 0; i < arraySize; i++)
                {
                    var elementProperty = arrayProperty.GetArrayElementAtIndex(i);
                    height += EditorGUI.GetPropertyHeight(elementProperty) + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            return height;
        }


        private void DrawElements(ref Rect position, SerializedProperty arrayProperty, string[] enumNames)
        {
            var arraySize = arrayProperty.arraySize;
            for (var i = 0; i < arraySize; i++)
            {
                var elementProperty = arrayProperty.GetArrayElementAtIndex(i);
                var elementHeight = EditorGUI.GetPropertyHeight(elementProperty);
                position.height = elementHeight;
                EditorGUI.PropertyField(position, elementProperty, new GUIContent(enumNames[i]), true);
                position.y += elementHeight  + EditorGUIUtility.standardVerticalSpacing;

            }
        }
    }
}