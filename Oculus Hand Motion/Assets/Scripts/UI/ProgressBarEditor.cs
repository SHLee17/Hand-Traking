using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProgressBar))]
public class ProgressBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ProgressBar progressBar = (ProgressBar)target;

        if (GUILayout.Button("Set"))
            progressBar.SetProgressBar();
        else if (GUILayout.Button("Clear"))
            progressBar.ClearProgressBar();
    }
}
