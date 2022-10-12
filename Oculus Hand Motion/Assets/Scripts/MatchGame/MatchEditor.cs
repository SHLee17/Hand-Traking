using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(MatchNumber))]
public class MatchEditor : Editor
{
    MatchNumber match;

    private void OnEnable()
    {
        match = (MatchNumber)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(400);

        DrawRect(match.blankList[1].isMatchActive, new Rect(7, 5, 20, 160));
        DrawRect(match.blankList[2].isMatchActive, new Rect(187, 5, 20, 160));
        DrawRect(match.blankList[4].isMatchActive, new Rect(7, 160, 20, 140));
        DrawRect(match.blankList[5].isMatchActive, new Rect(187, 160, 20, 140));
        DrawRect(match.blankList[6].isMatchActive, new Rect(7, 285, 200, 20));
        DrawRect(match.blankList[3].isMatchActive, new Rect(7, 145, 200, 20));
        DrawRect(match.blankList[0].isMatchActive, new Rect(7, 5, 200, 20));

        match.blankList[0].isMatchActive = EditorGUI.Toggle(new Rect(100, 10, 10, 10), match.blankList[0].isMatchActive);
        match.blankList[1].isMatchActive = EditorGUI.Toggle(new Rect(10, 80, 10, 10), match.blankList[1].isMatchActive);
        match.blankList[2].isMatchActive = EditorGUI.Toggle(new Rect(190, 80, 10, 10), match.blankList[2].isMatchActive);
        match.blankList[3].isMatchActive = EditorGUI.Toggle(new Rect(100, 150, 10, 10), match.blankList[3].isMatchActive);
        match.blankList[4].isMatchActive = EditorGUI.Toggle(new Rect(10, 220, 10, 10), match.blankList[4].isMatchActive);
        match.blankList[5].isMatchActive = EditorGUI.Toggle(new Rect(190, 220, 10, 10), match.blankList[5].isMatchActive);
        match.blankList[6].isMatchActive = EditorGUI.Toggle(new Rect(100, 290, 10, 10), match.blankList[6].isMatchActive);

        base.OnInspectorGUI();
    }

    public void DrawRect(bool isBool, Rect rect)
    {
        if(isBool)
            EditorGUI.DrawRect(rect, Color.red);
        else
            EditorGUI.DrawRect(rect, Color.white);
    }
}
