using OMG.UI;
using UnityEditor;
using UnityEngine;

namespace OMG.Editor
{
    [CustomEditor(typeof(GameCycleText))]
    public class GameCycleTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameCycleText text = (GameCycleText)target;

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("readyGo", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                text.PlayRaedyGo();
            }
            if (GUILayout.Button("FadeOut", GUILayout.Width(120f), GUILayout.Height(30f)))
            {
                text.PlayFinish();
            }

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();
        }
    }
}
