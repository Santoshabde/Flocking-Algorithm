using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        if (property.objectReferenceValue == null)
        {
            GUI.backgroundColor = Color.red;
            label.tooltip = "This is Required";
        }

        EditorGUI.PropertyField(position, property, label);
        EditorGUI.EndProperty();
    }
}
