using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TriangleWindowEditor : EditorWindow
{
    [SerializeField]
    Sprite sprTriangle;
    [SerializeField]
    TriangleStage stage;

    [SerializeField]
    TriangleStage currentStage;

    [MenuItem("Window/Match/Triangle")]
    static void Init()
    {
        TriangleWindowEditor window = (TriangleWindowEditor)GetWindow(typeof(TriangleWindowEditor));
        window.Show();
    }

    private void OnGUI()
    {
        currentStage = (TriangleStage)EditorGUI.ObjectField(new Rect(800, 10, 200,20),currentStage, typeof(TriangleStage), true);

        if (GUI.Button(new Rect(800, 40, 200, 50), "New Stage"))
            currentStage = Instantiate(stage);

        if (currentStage != null)
        {
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
                        GUI.DrawTexture(new Rect(j * sizeTriangle, i * sizeTriangle, sizeTriangle, sizeTriangle), sprTriangle.texture);

                        TriangleBlank(index, triangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(25 + sizeTriangle * j, 55 + sizeTriangle * i, 20, 20),
                            TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, 75 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, 95 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }

                        triangleIndex++;

                        TriangleBlank(index, triangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(110 + sizeTriangle * j, 55 + sizeTriangle * i, 20, 20),
                            TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, 75 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, 95 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }

                        triangleIndex++;
                        TriangleBlank(index, triangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(68 + sizeTriangle * j, 125 + sizeTriangle * i, 20, 20),
                           TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, 145 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, 165 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }
                    }
                    else
                    {
                        triangleIndex = 0;
                        GUI.DrawTexture(new Rect(j * sizeTriangle + (sizeTriangle / 2), i * sizeTriangle, sizeTriangle, sizeTriangle), sprTriangle.texture);

                        TriangleBlank(index, triangleIndex).gameObject.SetActive(
                           EditorGUI.Toggle(new Rect(25 + (sizeTriangle / 2) + sizeTriangle * j, 55 + sizeTriangle * i, 20, 20),
                           TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, 75 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, 95 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }

                        triangleIndex++;
                        TriangleBlank(index, triangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(110 + (sizeTriangle / 2) + sizeTriangle * j, 55 + sizeTriangle * i, 20, 20),
                            TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, 75 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, 95 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }
                        triangleIndex++;
                            TriangleBlank(index, triangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(68 + (sizeTriangle / 2) + sizeTriangle * j, 125 + sizeTriangle * i, 20, 20),
                            TriangleBlank(index, triangleIndex).gameObject.activeSelf));

                        if (TriangleBlank(index, triangleIndex).gameObject.activeSelf)
                        {
                            TriangleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, 145 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).activeMatch);

                            TriangleBlank(index, triangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, 165 + sizeTriangle * i, 50, 20),
                                TriangleBlank(index, triangleIndex).answer);
                        }
                    }
                }
            }
        }

    }

    Blank TriangleBlank(int stageIndex, int trianlgleIndex)
    {
        return currentStage.triangleList[stageIndex].triangle[trianlgleIndex];
    }
}
