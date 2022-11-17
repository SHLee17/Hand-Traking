using System.Drawing;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchWindowEditor : EditorWindow
{
    [SerializeField]
    Transform parent;
    [SerializeField]
    Sprite sprTriangle;
    [SerializeField]
    Sprite verticalRect;
    [SerializeField]
    Sprite horizontalRect;

    [SerializeField]
    MatchStage triangleStage;
    [SerializeField]
    MatchStage rectangleStage;

    [SerializeField]
    MatchStage currentStage;

    int tabIndex;
    string[] toolbars = { "Triangle", "Rectangle" };

    [Range(20, 40)]
    [SerializeField]
    int y = 150;

    [MenuItem("Window/Match/Angle")]
    static void Init()
    {
        MatchWindowEditor window = (MatchWindowEditor)GetWindow(typeof(MatchWindowEditor));
        window.position = new Rect(100, 100, 1100, 1000);
        window.Show();
    }

    private void OnGUI()
    {
        if (parent == null)
            parent = GameObject.FindGameObjectWithTag("GameManager").transform;
        currentStage = (MatchStage)EditorGUI.ObjectField(new Rect(850, y + 10, 200, 20), currentStage, typeof(MatchStage), true);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        tabIndex = GUILayout.Toolbar(tabIndex, toolbars, GUILayout.Width(200), GUILayout.Height(40));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        switch (tabIndex)
        {
            case 0:
                if (GUI.Button(new Rect(850, y + 40, 200, 50), "New Triangle Stage"))
                {
                    currentStage = Instantiate(triangleStage);
                    currentStage.transform.SetParent(parent);
                    currentStage.transform.localPosition = Vector3.zero;
                    currentStage.transform.localRotation = Quaternion.identity;
                }

                if (currentStage != null)
                {
                    if (MatchStage.Type.Triangle == currentStage.type)
                        Triangle();
                }
                break;
            case 1:
                if (GUI.Button(new Rect(850, y + 40, 200, 50), "New Rect Stage"))
                {
                    currentStage = Instantiate(rectangleStage);
                    currentStage.transform.SetParent(parent);
                    currentStage.transform.localPosition = Vector3.zero;
                    currentStage.transform.localRotation = Quaternion.identity;
                }

                if (currentStage != null)
                {
                    if (MatchStage.Type.Rectangle == currentStage.type)
                        Rectangle();
                }
                break;
        }
        if(currentStage != null)
        {
            EditorGUI.LabelField(new Rect(850, y + 100, 50, 20), "Hint");

            currentStage.txtHint.text = EditorGUI.TextArea(new Rect(850, y + 130, 200, 400), currentStage.txtHint.text);
        }
    }
    void Inventory()
    {
        GUI.Label(new Rect(40, 10, 100, 20), "Inventory");

        for (int i = 0; i < currentStage.inventoryList.Count; i++)
        {
            currentStage.inventoryList[i].gameObject.SetActive(
            EditorGUI.Toggle(new Rect((20 * i) + (35 * i) + 23, 60, 20, 20),
            currentStage.inventoryList[i].gameObject.activeSelf));

            if (currentStage.inventoryList[i].gameObject.activeSelf)
            {
                GUI.DrawTexture(new Rect((20 * i) + (35 * i) + 20, 40, 20, 100), verticalRect.texture);
                currentStage.inventoryList[i].activeMatch = (Blank.ActiveMatch)
                    EditorGUI.EnumPopup(new Rect((20 * i) + (35 * i), 80, 50, 20),
                     currentStage.inventoryList[i].activeMatch);

            }
        }
    }
    void Rectangle()
    {
        Inventory();
        int sizeRectangleWidth = 20;
        int sizeRectangleHeight = 120;

        int index = 0;

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                index = i;

                AngleBlank(index, j).gameObject.SetActive(
                EditorGUI.Toggle(new Rect(j * sizeRectangleHeight + (sizeRectangleHeight / 2) + 25, y + (sizeRectangleHeight * i) - 20, 20, 20),
                AngleBlank(index, j).gameObject.activeSelf));

                if (AngleBlank(index, j).gameObject.activeSelf)
                {
                    GUI.DrawTexture(new Rect(j * sizeRectangleHeight + 20, y + sizeRectangleHeight * i, sizeRectangleHeight, sizeRectangleWidth), horizontalRect.texture);

                    AngleBlank(index, j).activeMatch = (Blank.ActiveMatch)EditorGUI.EnumPopup(new Rect(j * sizeRectangleHeight - 20 + (sizeRectangleHeight / 2) + 25, 
                        y + (sizeRectangleHeight * i), 
                        50, 20),
                       AngleBlank(index, j).activeMatch);

                    AngleBlank(index, j).answer = (Blank.Answer)EditorGUI.EnumPopup(new Rect(j * sizeRectangleHeight - 20 + (sizeRectangleHeight / 2) + 25, 
                        y + (sizeRectangleHeight * i) + 20,
                        50, 20),
                         AngleBlank(index, j).answer);
                }
                else
                {
                    AngleBlank(index, j).activeMatch = Blank.ActiveMatch.deactive;
                    AngleBlank(index, j).answer = Blank.Answer.None;
                }


            }
        }


        for (int i = 0; i < 6; i++)
        {
            index++;
            for (int j = 0; j < 5; j++)
            {
                AngleBlank(index, j).gameObject.SetActive(
                  EditorGUI.Toggle(new Rect(i * sizeRectangleHeight + 25, y + (sizeRectangleHeight / 2) - 20 + sizeRectangleHeight * j, 20, 20),
                  AngleBlank(index, j).gameObject.activeSelf));

                if (AngleBlank(index, j).gameObject.activeSelf)
                {
                    GUI.DrawTexture(new Rect(i * sizeRectangleHeight + 20, y + sizeRectangleHeight * j, sizeRectangleWidth, sizeRectangleHeight), verticalRect.texture);
                    AngleBlank(index, j).activeMatch = (Blank.ActiveMatch)EditorGUI.EnumPopup(new Rect(i * sizeRectangleHeight, 
                        y + (sizeRectangleHeight / 2) + sizeRectangleHeight * j, 
                        50, 20),
                        AngleBlank(index, j).activeMatch);

                    AngleBlank(index, j).answer = (Blank.Answer)EditorGUI.EnumPopup(new Rect(i * sizeRectangleHeight, 
                        y + 20 + (sizeRectangleHeight / 2) + sizeRectangleHeight * j,
                        50, 20),
                        AngleBlank(index, j).answer);
                }
                else
                {
                    AngleBlank(index, j).activeMatch = Blank.ActiveMatch.deactive;
                    AngleBlank(index, j).answer = Blank.Answer.None;
                }
            }
        }
    }
    void Triangle()
    {
        Inventory();
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

                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(25 + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(10 + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }

                    triangleIndex++;

                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(110 + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(95 + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }

                    triangleIndex++;
                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(68 + sizeTriangle * j, y + 125 + sizeTriangle * i, 20, 20),
                       AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, y + 145 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(53 + sizeTriangle * j, y + 165 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }
                }
                else
                {
                    triangleIndex = 0;
                    GUI.DrawTexture(new Rect(j * sizeTriangle + (sizeTriangle / 2), i * sizeTriangle + y, sizeTriangle, sizeTriangle), sprTriangle.texture);

                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                       EditorGUI.Toggle(new Rect(25 + (sizeTriangle / 2) + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                       AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(10 + (sizeTriangle / 2) + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }

                    triangleIndex++;
                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                        EditorGUI.Toggle(new Rect(110 + (sizeTriangle / 2) + sizeTriangle * j, y + 55 + sizeTriangle * i, 20, 20),
                        AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, y + 75 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(95 + (sizeTriangle / 2) + sizeTriangle * j, y + 95 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }
                    triangleIndex++;
                    AngleBlank(index, triangleIndex).gameObject.SetActive(
                    EditorGUI.Toggle(new Rect(68 + (sizeTriangle / 2) + sizeTriangle * j, y + 125 + sizeTriangle * i, 20, 20),
                    AngleBlank(index, triangleIndex).gameObject.activeSelf));

                    if (AngleBlank(index, triangleIndex).gameObject.activeSelf)
                    {
                        AngleBlank(index, triangleIndex).activeMatch = (Blank.ActiveMatch)
                            EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, y + 145 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).activeMatch);

                        AngleBlank(index, triangleIndex).answer = (Blank.Answer)
                            EditorGUI.EnumPopup(new Rect(53 + (sizeTriangle / 2) + sizeTriangle * j, y + 165 + sizeTriangle * i, 50, 20),
                            AngleBlank(index, triangleIndex).answer);
                    }
                    else
                    {
                        AngleBlank(index, triangleIndex).activeMatch = Blank.ActiveMatch.deactive;
                        AngleBlank(index, triangleIndex).answer = Blank.Answer.None;
                    }
                }
            }

        }

    }
    Blank AngleBlank(int stageIndex, int trianlgleIndex)
    {
        return currentStage.stage[stageIndex].angleList[trianlgleIndex];
    }
    public override void SaveChanges()
    {
        base.SaveChanges();
    }
    private void OnDestroy()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        SaveChanges();
        Debug.Log("SAVE");
    }
}
