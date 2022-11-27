using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public float current;
    public float max;
    public float normalized;
    [SerializeField]
    bool isStart;
    [SerializeField]
    int cubeCount;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    ProgressBarCube cube;
    [SerializeField]
    List<ProgressBarCube> cubeList;

    private void Start()
    {
        cubeCount = cubeList.Count;
    }

    void Update()
    {
        if (isStart)
        {
            normalized = current / max;
            
            if ((normalized * cubeList.Count) < cubeCount)
            {
                if (cubeCount > 0)
                {
                    cubeList[cubeCount - 1].gameObject.SetActive(false);
                    cubeCount--;
                }
            }

            if (normalized > 0.8f) SetColor(Color.blue);
            else if (normalized > 0.5f) SetColor(Color.green);
            else if (normalized > 0.3f) SetColor(Color.yellow);
            else if (normalized > 0.1f) SetColor(Color.red);
            else if (normalized < 0)
            {
                gameObject.SetActive(false);
                isStart = false;
            }
        }
    }

    public void Set(float current, float max)
    {
        this.current = current;
        this.max = max;
    }

    public void StartProtress()
    {
        foreach (ProgressBarCube item in cubeList)
        {
            item.gameObject.SetActive(true);
            item.barRenderer.material.color = Color.blue;
        }
        cubeCount = cubeList.Count;

        isStart = true;
    }

    public void SetColor(Color color)
    {
        foreach (ProgressBarCube item in cubeList)
            item.barRenderer.material.color = color;
    }

    public void SetProgressBar()
    {
        for (int i = 0; i < 20; i++)
        {
            ProgressBarCube progressBarCube = Instantiate(cube, transform);

            progressBarCube.transform.localPosition = new Vector3(i * 0.025f, 0, 0);
            cubeList.Add(progressBarCube);
        }
    }

    public void ClearProgressBar()
    {
        foreach (ProgressBarCube item in cubeList)
            DestroyImmediate(item.gameObject);
        cubeList.Clear();
    }


}
