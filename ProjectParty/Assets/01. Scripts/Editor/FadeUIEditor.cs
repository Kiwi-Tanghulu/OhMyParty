using UnityEngine;
using UnityEditor;
using OMG.UI;

namespace OMG.Editor
{
    [CustomEditor(typeof(FadeUI))]
    public class FadeUIEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            FadeUI fadeUi = (FadeUI)target;

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if(GUILayout.Button("FadeIn", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                fadeUi.FadeIn();
            }
            if (GUILayout.Button("FadeOut", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                fadeUi.FadeOut();
            }

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }
    }
}
