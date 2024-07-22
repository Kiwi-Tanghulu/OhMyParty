using UnityEngine;
using UnityEditor;
using OMG.UI;

namespace OMG.Attributes
{
    [CustomEditor(typeof(Fade))]
    public class FadeUIEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Fade fadeUi = (Fade)target;

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if(GUILayout.Button("FadeIn", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                fadeUi.FadeIn(0f);
            }
            if (GUILayout.Button("FadeOut", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                fadeUi.FadeOut(0f);
            }

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }
    }
}
