using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RectangleWindowEditor : EditorWindow
{
    [SerializeField]
    Sprite sprRectangle;
    [SerializeField]
    RectangleStage stage;

    [SerializeField]
    RectangleStage currentStage;

    [MenuItem("Window/Match/Rectangle")]
    static void Init()
    {
        RectangleWindowEditor window = (RectangleWindowEditor)GetWindow(typeof(RectangleWindowEditor));
        window.Show();
    }

    private void OnGUI()
    {
        currentStage = (RectangleStage)EditorGUI.ObjectField(new Rect(800, 10, 200,20),currentStage, typeof(RectangleStage), true);

        if (GUI.Button(new Rect(800, 40, 200, 50), "New Stage"))
            currentStage = Instantiate(stage);

        if (currentStage != null)
        {
            int rectangleIndex;
            int sizeRectangle = 150;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int index = i * 5 + j;
                    if (i % 2 == 0)
                    {
                        rectangleIndex = 0;
                        GUI.DrawTexture(new Rect(j * sizeRectangle, i * sizeRectangle, sizeRectangle, sizeRectangle), sprRectangle.texture);

                        RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(25 + sizeRectangle * j, 55 + sizeRectangle * i, 20, 20),
                            RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(10 + sizeRectangle * j, 75 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(10 + sizeRectangle * j, 95 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }

                        rectangleIndex++;

                        RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(110 + sizeRectangle * j, 55 + sizeRectangle * i, 20, 20),
                            RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(95 + sizeRectangle * j, 75 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(95 + sizeRectangle * j, 95 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }

                        rectangleIndex++;
                        RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(68 + sizeRectangle * j, 125 + sizeRectangle * i, 20, 20),
                           RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(53 + sizeRectangle * j, 145 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(53 + sizeRectangle * j, 165 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }
                    }
                    else
                    {
                        rectangleIndex = 0;
                        GUI.DrawTexture(new Rect(j * sizeRectangle + (sizeRectangle / 2), i * sizeRectangle, sizeRectangle, sizeRectangle), sprRectangle.texture);

                        RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                           EditorGUI.Toggle(new Rect(25 + (sizeRectangle / 2) + sizeRectangle * j, 55 + sizeRectangle * i, 20, 20),
                           RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(10 + (sizeRectangle / 2) + sizeRectangle * j, 75 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(10 + (sizeRectangle / 2) + sizeRectangle * j, 95 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }

                        rectangleIndex++;
                        RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(110 + (sizeRectangle / 2) + sizeRectangle * j, 55 + sizeRectangle * i, 20, 20),
                            RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(95 + (sizeRectangle / 2) + sizeRectangle * j, 75 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(95 + (sizeRectangle / 2) + sizeRectangle * j, 95 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }
                        rectangleIndex++;
                            RectangleBlank(index, rectangleIndex).gameObject.SetActive(
                            EditorGUI.Toggle(new Rect(68 + (sizeRectangle / 2) + sizeRectangle * j, 125 + sizeRectangle * i, 20, 20),
                            RectangleBlank(index, rectangleIndex).gameObject.activeSelf));

                        if (RectangleBlank(index, rectangleIndex).gameObject.activeSelf)
                        {
                            RectangleBlank(index, rectangleIndex).activeMatch = (Blank.ActiveMatch)
                                EditorGUI.EnumPopup(new Rect(53 + (sizeRectangle / 2) + sizeRectangle * j, 145 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).activeMatch);

                            RectangleBlank(index, rectangleIndex).answer = (Blank.Answer)
                                EditorGUI.EnumPopup(new Rect(53 + (sizeRectangle / 2) + sizeRectangle * j, 165 + sizeRectangle * i, 50, 20),
                                RectangleBlank(index, rectangleIndex).answer);
                        }
                    }
                }
            }
        }

    }

    Blank RectangleBlank(int stageIndex, int rectangleIndex)
    {
        return currentStage.rectangleList[stageIndex].rectangle[rectangleIndex];
    }
}
