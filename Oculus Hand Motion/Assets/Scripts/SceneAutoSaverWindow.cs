#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ?†Ïßú : 2021-03-08 AM 1:38:56
// ?ëÏÑ±??: Rito

namespace Rito
{
    public class SceneAutoSaverWindow : EditorWindow
    {
        [MenuItem("Window/Rito/Scene Auto Saver")]
        private static void Init()
        {
            // ?ÑÏû¨ ?úÏÑ±?îÎêú ?àÎèÑ??Í∞Ä?∏Ïò§Î©? ?ÜÏúºÎ©??àÎ°ú ?ùÏÑ±
            SceneAutoSaverWindow window = (SceneAutoSaverWindow)GetWindow(typeof(SceneAutoSaverWindow));
            window.Show();
            window.titleContent.text = "Scene Auto Saver";

            window.minSize = new Vector2(340f, 150f);
            window.maxSize = new Vector2(400f, 180f);
        }

        void OnGUI()
        {
            Color originColor = EditorStyles.boldLabel.normal.textColor;
            EditorStyles.boldLabel.normal.textColor = Color.yellow;

            EditorGUI.BeginChangeCheck();

            // ============================================================================ Options ==
            GUILayout.Space(10f);
            GUILayout.Label("Options", EditorStyles.boldLabel);

            SceneAutoSaver.Activated = EditorGUILayout.Toggle("On", SceneAutoSaver.Activated);
            SceneAutoSaver.ShowLog = EditorGUILayout.Toggle("Show Log", SceneAutoSaver.ShowLog);
            SceneAutoSaver.SaveCycle = EditorGUILayout.DoubleField("Save Cycle (sec)", SceneAutoSaver.SaveCycle);

            // ============================================================================ Logs ==
            GUILayout.Space(10f);
            GUILayout.Label("Logs", EditorStyles.boldLabel);

            // ============================================================================ Last Saved Time ==
            GUILayout.BeginHorizontal();

            GUILayout.Label("Last Saved Time :");
            GUILayout.Label(SceneAutoSaver.LastSavedTimeForLog.ToString("[yyyy-MM-dd] hh : mm : ss"));

            GUILayout.EndHorizontal();

            // ============================================================================ Next Save Remaining ==
            GUILayout.BeginHorizontal();

            GUILayout.Label("Next Save :");
            GUILayout.Label(SceneAutoSaver.NextSaveRemaining.ToString("00.00"));

            GUILayout.EndHorizontal();
            // ============================================================================

            if (EditorGUI.EndChangeCheck())
            {
                SceneAutoSaver.SaveOptions();
            }

            EditorStyles.boldLabel.normal.textColor = originColor;
        }
    }
}

#endif