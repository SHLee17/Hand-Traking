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


    }
    void Update()
    {
        timer -= Time.deltaTime;

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

    
    void SetLevelText()
    {
        int index = 0;

        for (int i = 0; i < padLineList.Count; i++)
        {
            if (i < gameLevel)
            {
                for (int j = 0; j < padLineList[i].padList.Count; j++)
                {
                    if (j < gameLevel)
                    {
                        index++;
                        padLineList[i].padList[j].txtNumber.text = index.ToString();
                    }
                    else
                        continue;

                }
            }
            else
                continue;
        }
    }

}
