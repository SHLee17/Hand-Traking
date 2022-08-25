using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumGameManager : MonoBehaviour
{
    public Transform[] fingerPoses;
    public List<Pose> poseList;
    
    [Header("Text")]
    public TMP_Text[] txtDirection;
    public TMP_Text txtTimer;

    int leftCount, rightCount;
    float timer;

    void Start()
    {
        timer = 30;
        foreach (Transform item in fingerPoses)
        {
            foreach (Transform child in item)
                poseList.Add(child.GetComponent<Pose>());
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            txtTimer.text = timer.ToString("N1");
            
        }

        CheckCount();
    }
    public void CheckCount()
    {
        leftCount = 0;
        rightCount = 0;
        foreach (Pose item in poseList)
        {
            if(item.dir == Direction.Left)
            {
                if(item.select)
                    leftCount += 1;
            }
            else
            {
                if (item.select)
                    rightCount += 1;
            }
        }
        txtDirection[0].text = leftCount.ToString();
        txtDirection[1].text = rightCount.ToString();
    }

}
