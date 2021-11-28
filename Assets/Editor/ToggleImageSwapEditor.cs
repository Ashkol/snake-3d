namespace AshkolTools.UI.Editor
{
    using AshkolTools.UI;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ToggleImageSwap))]
    public class ToggleImageSwapEditor : UnityEditor.UI.ToggleEditor
    {
        SerializedProperty onImage;
        SerializedProperty offImage;

        protected override void OnEnable()
        {
            base.OnEnable();
            onImage = serializedObject.FindProperty("onImage");
            offImage = serializedObject.FindProperty("offImage");
        }

        public override void OnInspectorGUI()
        {
            ToggleImageSwap component = (ToggleImageSwap)target;

            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(onImage);
            EditorGUILayout.PropertyField(offImage);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
