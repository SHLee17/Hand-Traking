#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

#pragma warning disable CS0618 // Obsolete

// ?†Ïßú : 2021-03-08 AM 1:12:05
// ?ëÏÑ±??: Rito

namespace Rito
{
    /// <summary> Ï£ºÍ∏∞?ÅÏúºÎ°??ÑÏû¨ ???êÎèô ?Ä??</summary>
    [InitializeOnLoad]
    public static class SceneAutoSaver
    {
        private const string Prefix = "SAS_";

        public static bool Activated { get; set; }
        public static bool ShowLog { get; set; }
        public static double SaveCycle
        {
            get => _saveCycle;
            set
            {
                if(value < 5f) value = 5f;
                _saveCycle = value;
            }
        }
        public static DateTime LastSavedTimeForLog { get; private set; } // ÏµúÍ∑º ?Ä???úÍ∞Ñ(Î≥¥Ïó¨Ï£ºÍ∏∞??
        public static double NextSaveRemaining { get; private set; }

        private static double _saveCycle = 10f;
        private static DateTime _lastSavedTime; // ÏµúÍ∑º ?Ä???úÍ∞Ñ
        
        // ?ïÏ†Å ?ùÏÑ±??: ?êÎîî??Update ?¥Î≤§?∏Ïóê ?∏Îì§???±Î°ù
        static SceneAutoSaver()
        {
            var handlers = EditorApplication.update.GetInvocationList();

            bool hasAlready = false;
            foreach (var handler in handlers)
            {
                if(handler.Method.Name == nameof(UpdateAutoSave))
                    hasAlready = true;
            }

            if(!hasAlready)
                EditorApplication.update += UpdateAutoSave;

            _lastSavedTime = LastSavedTimeForLog = DateTime.Now;

            LoadOptions();
        }

        public static void SaveOptions()
        {
            EditorPrefs.SetBool(Prefix + nameof(Activated), Activated);
            EditorPrefs.SetBool(Prefix + nameof(ShowLog), ShowLog);
            EditorPrefs.SetFloat(Prefix + nameof(SaveCycle), (float)SaveCycle);
        }

        private static void LoadOptions()
        {
            Activated = EditorPrefs.GetBool(Prefix + nameof(Activated), true);
            ShowLog   = EditorPrefs.GetBool(Prefix + nameof(ShowLog), true);
            SaveCycle = EditorPrefs.GetFloat(Prefix + nameof(SaveCycle), 10f);

            // ?åÏàò???êÏûêÎ¶?Ïª?
            SaveCycle = Math.Floor(SaveCycle * 100.0) * 0.01;
        }
        
        // ?úÍ∞Ñ??Ï≤¥ÌÅ¨?òÏó¨ ?êÎèô ?Ä??
        private static void UpdateAutoSave()
        {
            DateTime dtNow = DateTime.Now;

            if (!Activated || EditorApplication.isPlaying || !EditorApplication.isSceneDirty)
            {
                _lastSavedTime = dtNow;
                NextSaveRemaining = _saveCycle;
                return;
            }

            // ?úÍ∞Ñ Í≥ÑÏÇ∞
            double diff = dtNow.Subtract(_lastSavedTime).TotalSeconds;

            NextSaveRemaining = SaveCycle - diff;
            if(NextSaveRemaining < 0f) NextSaveRemaining = 0f;

            // ?ïÌï¥Ïß??úÍ∞Ñ Í≤ΩÍ≥º ???Ä??Î∞?ÏµúÍ∑º ?Ä???úÍ∞Ñ Í∞±Ïã†
            if (diff > SaveCycle)
            {
                //if(EditorApplication.isSceneDirty)
                EditorSceneManager.SaveOpenScenes();
                _lastSavedTime = LastSavedTimeForLog = dtNow;

                if (ShowLog)
                {
                    string dateStr = dtNow.ToString("yyyy-MM-dd  hh:mm:ss");
                    UnityEngine.Debug.Log($"[Auto Save] {dateStr}");
                }
            }
        }
    }
}

#endif