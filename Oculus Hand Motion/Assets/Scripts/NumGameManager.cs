using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class NumGameManager : MonoBehaviour
{
    enum Order
    {
        first = 1,
        second,
        third
    }

    [System.Serializable]
    public class Number
    {
        public TMP_Text txtNum;
        public int num;
    }

    public Transform[] fingerPoses;
    public List<Pose> poseList;
    public List<Number> numList;

    [Header("Text")]
    public TMP_Text[] txtDirections;
    public TMP_Text txtTimer;
    public TMP_Text txtSign;

    Order order;
    int leftCount, rightCount;
    Dictionary<Order, Number> answerDict;
    float timer;
    bool isSetExamQuestions;

    void Start()
    {
        answerDict.Add(Order.first, numList[0]);
        answerDict.Add(Order.second, numList[1]);
        answerDict.Add(Order.third, numList[2]);

        timer = 30;
        foreach (Transform item in fingerPoses)
        {
            foreach (Transform child in item)
                poseList.Add(child.GetComponent<Pose>());
        }
    }

    void Update()
    {
        if(!isSetExamQuestions)
        {
            order = (Order)RandRange(1,4);

            switch (order)
            {
                case Order.first:

                    break;
                case Order.second:
                    break;
                case Order.third:
                    break;
            }

            isSetExamQuestions = true;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            txtTimer.text = timer.ToString("N1");
            
        }

        CheckCount();
    }

    int RandRange(int i, int j)
    {
        return Random.Range(i, j); ;
    }
    void CheckCount()
    {
        foreach (Pose item in poseList)
        {
            if(item.dir == Direction.Left)
            {
                if(item.select)
                    leftCount = item.num;
            }
            else
            {
                if (item.select)
                    rightCount = item.num;
            }
        }
        txtDirections[0].text = leftCount.ToString();
        txtDirections[1].text = rightCount.ToString();


        string answer = (leftCount + rightCount).ToString();

        switch (order)
        {
            case Order.first:

                break;
            case Order.second:
                break;
            case Order.third:
                break;
        }

    }



}
