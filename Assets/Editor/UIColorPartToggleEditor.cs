namespace MobileGame.Snake
{
    
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(UIColorPartToggle))]
    public class UIColorPartToggleEditor : UnityEditor.UI.ToggleEditor
    {
        SerializedProperty label;
        SerializedProperty hexadecimalColorValue;

        protected override void OnEnable()
        {
            base.OnEnable();
            label = serializedObject.FindProperty("label");
            hexadecimalColorValue = serializedObject.FindProperty("hexadecimalColorValue");
        }

        public override void OnInspectorGUI()
        {
            UIColorPartToggle component = (UIColorPartToggle)target;

            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(label);
            EditorGUILayout.PropertyField(hexadecimalColorValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
