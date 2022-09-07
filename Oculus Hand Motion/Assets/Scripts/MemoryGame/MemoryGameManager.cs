using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    [System.Serializable]
    public struct Matrix
    {
        public int i, j;
        public Matrix(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }

    [System.Serializable]
    public class PadArray2D
    {
        public List<Pad> padList;
    }
    public int gameLevel;
    public List<PadArray2D> padLineList;

    [SerializeField]
    List<Matrix> playerPadList;
    [SerializeField]
    List<Matrix> randPadList;
    [SerializeField]
    List<PadButton> padButtonList;

    int count;
    float timer;
    float timerReset;
    float padLightSpeed;

    void Start()
    {
        playerPadList = new List<Matrix>();
        randPadList = new List<Matrix>();
        gameLevel = 3;
        timer = timerReset = 1.5f;
        count = 5;
        padLightSpeed = 0.5f;
        SetLevel();
    }
    void Update()
    {
        timer -= Time.deltaTime;

        Vector3 cameraPos = GameManager.Instance.player.cameraRig.centerEyeAnchor.transform.position + 
            new Vector3(-0.1f, -0.2f, 0.65f);

        transform.position = cameraPos;

        if (count > 0)
        {
            if (timer < 0)
            {

                int lineIndex = Random.Range(0, gameLevel);
                int padIndex = Random.Range(0, gameLevel);

                padLineList[lineIndex].padList[padIndex].LightSwitch(padLightSpeed);

                randPadList.Add(new Matrix(lineIndex, padIndex));

                timer = timerReset;
                count -= 1;
            }
        }
    }

    public void PadOnClick(int index)
    {

        int i = index / gameLevel;
        int j = index % gameLevel;

        playerPadList.Add(new Matrix(i, j));
    }

    int ConvertArrayIndex(int i, int j)
    {
        return (i * 5) + (j % 5);
    }
    void SetLevel()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i >= gameLevel) continue;
            for (int j = 0; j < 5; j++)
            {
                if (j < gameLevel)
                {
                    padButtonList[ConvertArrayIndex(i, j)].gameObject.SetActive(true);
                }
            }
        }

    }

}
