#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

#pragma warning disable CS0618 // Obsolete

// ? μ§ : 2021-03-08 AM 1:12:05
// ?μ±??: Rito

namespace Rito
{
    /// <summary> μ£ΌκΈ°?μΌλ‘??μ¬ ???λ ???</summary>
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
        public static DateTime LastSavedTimeForLog { get; private set; } // μ΅κ·Ό ????κ°(λ³΄μ¬μ£ΌκΈ°??
        public static double NextSaveRemaining { get; private set; }

        private static double _saveCycle = 10f;
        private static DateTime _lastSavedTime; // μ΅κ·Ό ????κ°
        
        // ?μ  ?μ±??: ?λ??Update ?΄λ²€?Έμ ?Έλ€???±λ‘
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

            // ?μ???μλ¦?μ»?
            SaveCycle = Math.Floor(SaveCycle * 100.0) * 0.01;
        }
        
        // ?κ°??μ²΄ν¬?μ¬ ?λ ???
        private static void UpdateAutoSave()
        {
            DateTime dtNow = DateTime.Now;

            if (!Activated || EditorApplication.isPlaying || !EditorApplication.isSceneDirty)
            {
                _lastSavedTime = dtNow;
                NextSaveRemaining = _saveCycle;
                return;
            }

            // ?κ° κ³μ°
            double diff = dtNow.Subtract(_lastSavedTime).TotalSeconds;

            NextSaveRemaining = SaveCycle - diff;
            if(NextSaveRemaining < 0f) NextSaveRemaining = 0f;

            // ?ν΄μ§??κ° κ²½κ³Ό ?????λ°?μ΅κ·Ό ????κ° κ°±μ 
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