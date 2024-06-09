using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OMG.Editors
{
    [CustomEditor(typeof(Object), true)]
    public class HideParentFieldEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            object[] hideParentFieldsAttributes = target.GetType().GetCustomAttributes(typeof(HideParentFieldsAttribute), true);
            string[] fieldsToHide = null;
            if(hideParentFieldsAttributes.Length > 0)
                fieldsToHide = (hideParentFieldsAttributes[0] as HideParentFieldsAttribute).FieldNames;

            SerializedProperty property = serializedObject.GetIterator();
            bool enterChildren = true;

            while (property.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (fieldsToHide != null && fieldsToHide.Contains(property.name))
                    continue;

                EditorGUILayout.PropertyField(property, true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
