using System;
using UnityEditor;
using UnityEngine;

public class MatchWindowEditor : EditorWindow
{
    [SerializeField]
    Sprite sprTriangle;
    [SerializeField]
    Sprite verticalRect;
    [SerializeField]
    Sprite horizontalRect;

    [SerializeField]
    MatchStage stage;

    [SerializeField]
    MatchStage currentStage;

    int tabIndex = 1;
    string[] toolbars = { "Triangle", "Rectangle" };

    [Range(20, 40)]
    [SerializeField]
    int y = 100;

    [MenuItem("Window/Match/Angle")]
    static void Init()
    {
        MatchWindowEditor window = (MatchWindowEditor)GetWindow(typeof(MatchWindowEditor));
        window.Show();
    }

    private void OnGUI()
    {

        GUILayout.BeginArea(new Rect(0, 0, 1400, 1400));
        currentStage = (MatchStage)EditorGUI.ObjectField(new Rect(800, y + 10, 200, 20), currentStage, typeof(MatchStage), true);

        if (GUI.Button(new Rect(800, y + 40, 200, 50), "New Stage"))
            currentStage = Instantiate(stage);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        tabIndex = GUILayout.Toolbar(tabIndex, toolbars, GUILayout.Width(200), GUILayout.Height(40));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        switch (tabIndex)
        {
            case 0:
                if (currentStage != null)
                {
                    if(MatchStage.Type.Triangle == currentStage.type)
                    Triangle();
                }
                break;
            case 1:
                if (currentStage != null)
                {
                    if (MatchStage.Type.Rectangle == currentStage.type)
                        Rectangle();
                }
                break;
        }
        GUILayout.EndArea();


    }

    void Rectangle()
    {
        GUILayout.Label("Rectangle");

        int sizeRectangleWidth = 20;
        int sizeRectangleHeight = 100;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GUI.DrawTexture(new Rect(j * sizeRectangleHeight, y + sizeRectangleHeight * i, sizeRectangleWidth, sizeRectangleHeight), verticalRect.texture);



            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GUI.DrawTexture(new Rect(j * sizeRectangleHeight, y + sizeRectangleHeight * i, sizeRectangleHeight, sizeRectangleWidth), horizontalRect.texture);



            }
        }




    }

    void Triangle()
    {
        GUILayout.Label("Triangle");



        int triangleIndex;
        int sizeTriangle = 150;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int index = i * 5 + j;
                if (i % 2 == 0)
                {
                    triangleIndex = 0;
                    GUI.DrawTexture(new Rect(j * sizeTriangle, i * sizeTriangle + y, sizeTriangle, sizeTriangle), sprTriangle.texture);

                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(25 + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }

                    triangleIndex++;

                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(110 + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }

                    triangleIndex++;
                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(68 + sizeTriangle * j, y + 125 + sizeTriangle * i, 20, 20),
                       TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, y + 145 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, y + 165 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }
                }
                else
                {
                    triangleIndex = 0;
                    GUI.DrawTexture(new Rect(j * sizeTriangle + (sizeTriangle / 2), i * sizeTriangle, sizeTriangle, sizeTriangle), sprTriangle.texture);

                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                       EditorGUI.Toggle(new Rect(25 + (sizeTriangle / 2) + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                       TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }

                    triangleIndex++;
                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(110 + (sizeTriangle / 2) + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }
                    triangleIndex++;
                    TriangleBlank(index, triangleIndex).gameObject.SetActive(
                    EditorGUI.Toggle(new Rect(68 + (sizeTriangle / 2) + sizeTriangle * j, y + 125 + sizeTriangle * i, 20, 20),
                    TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, y + 145 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).activeMatch);

                        TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, y + 165 + sizeTriangle * i, 50, 20),
                            TriangleBlank(index, triangleIndex).answer);
                    }
                }
            }

        }

    }


    Blank TriangleBlank(int stageIndex, int trianlgleIndex)
    {
        return currentStage.stage[stageIndex].angleList[trianlgleIndex];
    }
}
