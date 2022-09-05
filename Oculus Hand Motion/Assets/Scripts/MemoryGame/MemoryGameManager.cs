using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    [System.Serializable]
    public class Array2D
    {
        public List<Pad> padList;
    }
    public int gameLevel;
    public List<Array2D> padLineList;

    float timer;
    void Start()
    {
        timer = 1.5f;
    }
    void Update()
    {
        timer -= Time.deltaTime; 

        if(timer < 0)
        {
            int lineIndex = Random.Range(0, 3);
           int padIndex = Random.Range(0, 3);

            padLineList[lineIndex].padList[padIndex].LightSwitch();

            timer = 2f;

        }

    }
}
