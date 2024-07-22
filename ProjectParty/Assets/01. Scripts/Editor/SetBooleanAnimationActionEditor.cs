using OMG.Player.FSM;
using UnityEditor;
using UnityEngine;

namespace OMG.Attributes
{
    [CustomEditor(typeof(SetBooleanAnimationAction))]
    public class SetBooleanAnimationActionEditor : UnityEditor.Editor
    {
        private SerializedProperty proertyName;

        private SerializedProperty setOnEnter;
        private SerializedProperty setOnExit;

        private SerializedProperty onEnterValue;
        private SerializedProperty onExitValue;

        private void OnEnable()
        {
            proertyName = serializedObject.FindProperty("proertyName");

            setOnEnter = serializedObject.FindProperty("setOnEnter");   
            setOnExit = serializedObject.FindProperty("setOnExit");

            onEnterValue = serializedObject.FindProperty("onEnterValue");
            onExitValue = serializedObject.FindProperty("onExitValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SetBooleanAnimationAction setBooleanAnimationAction = (SetBooleanAnimationAction)target;

            EditorGUILayout.PropertyField(proertyName);

            GUILayout.Space(10);
            EditorGUILayout.PropertyField(setOnEnter);
            EditorGUILayout.PropertyField(setOnExit);

            if(setBooleanAnimationAction.setOnEnter 
                || setBooleanAnimationAction.setOnExit)
            GUILayout.Space(10f);

            if(setBooleanAnimationAction.setOnEnter)
            {
                EditorGUILayout.PropertyField(onEnterValue);
            }
            if (setBooleanAnimationAction.setOnExit)
            {
                EditorGUILayout.PropertyField(onExitValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}