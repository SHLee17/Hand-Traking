using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class NumGameManager : MonoBehaviour
{
    enum Order
    {
        First,
        Second,
        Third
    }
    enum Sign
    {
        Add,
        Subtract,
        Divide,
        Multiply
    }

    [System.Serializable]
    public class Number
    {
        public TMP_Text txtNum;
        int num;

        public int Num
        {
            get => num;
            set
            {
                num = value;
                txtNum.text = num.ToString();
            }
        }
    }

    [Header("Arrays")]
    public Transform[] fingerPoses;
    public Number[] nums;
    [SerializeField]
    List<Pose> poseList;

    [Header("Texts")]
    public TMP_Text[] txtDirections;
    public TMP_Text txtTimer;
    public TMP_Text txtSign;

    [Header("Enums")]
    [SerializeField]
    Sign sign;
    [SerializeField]
    Order blankOrder;

    [Header("Data Type")]
    int leftCount, rightCount;
    float timer;
    [SerializeField]
    int answer;
    [SerializeField]
    bool isSetExamQuestions;
    Dictionary<Order, Number> answerDict;


    void Start()
    {
        answerDict = new Dictionary<Order, Number>();
        answerDict.Add(Order.First, nums[0]);
        answerDict.Add(Order.Second, nums[1]);
        answerDict.Add(Order.Third, nums[2]);

        timer = 30;
        foreach (Transform item in fingerPoses)
        {
            foreach (Transform child in item)
                poseList.Add(child.GetComponent<Pose>());
        }
    }

    void Update()
    {
        if (!isSetExamQuestions)
        {
            blankOrder = RandomEnum<Order>(1);
            sign = RandomEnum<Sign>();

            switch (sign)
            {
                case Sign.Add:
                    txtSign.text = "+";
                    break;
                case Sign.Subtract:
                    txtSign.text = "-";
                    break;
                case Sign.Divide:
                    txtSign.text = "¡À";
                    break;
                case Sign.Multiply:
                    txtSign.text = "x";
                    break;
            }

            ExampleQuestion(sign, blankOrder);
            isSetExamQuestions = true;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            txtTimer.text = timer.ToString("N1");

            CheckCount();
        }

    }
    void ExampleQuestion(Sign sign, Order blankOrder)
    {
        answerDict[blankOrder].Num = 0;

        void NumSet(Order order, int randNum) => answerDict[order].Num = randNum;
        int NumGet(Order order) { return answerDict[order].Num; }

        switch (sign)
        {
            case Sign.Add:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.First, RandRange(1, 10));
                    NumSet(Order.Third, RandRange(NumGet(Order.First) + 1, 20));
                    answer = NumGet(Order.Third) - NumGet(Order.First);
                }
                else
                {
                    NumSet(Order.First, RandRange(1, 9));
                    answer = RandRange(NumGet(Order.First) + 1, 10);
                    NumSet(Order.Second, answer - NumGet(Order.First));
                }
                break;

            case Sign.Subtract:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.First, RandRange(1, 100));
                    answer = RandRange(1, 10);
                    NumSet(Order.Third, NumGet(Order.First) - answer);
                }
                else
                {
                    answer = RandRange(1, 10);
                    NumSet(Order.First, RandRange(1, 100));
                    NumSet(Order.Second, NumGet(Order.First) - answer);
                }
                break;

            case Sign.Divide:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.Third, RandRange(1, 9));
                    answer = RandRange(1, 10);
                    NumSet(Order.First, NumGet(Order.Third) * answer);

                }
                else
                {
                    answer = RandRange(1, 10);
                    NumSet(Order.Second, RandRange(1, 9));
                    NumSet(Order.First, NumGet(Order.Second) * answer);
                }
                break;

            case Sign.Multiply:
                if (blankOrder == Order.Second)
                {
                    answer = RandRange(1, 10);
                    NumSet(Order.First, RandRange(1, 19));
                    NumSet(Order.Third, answer * NumGet(Order.First));
                }
                else
                {
                    answer = RandRange(1, 10);
                    NumSet(Order.First, RandRange(1, answer));
                    NumSet(Order.Second, answer / NumGet(Order.First));
                }
                    break;
        }

        Debug.Log(answer);
    }
    T RandomEnum<T>(int min = 0)
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(new System.Random().Next(min, values.Length));
    }
    int RandRange(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
    void CheckCount()
    {
        foreach (Pose item in poseList)
        {
            if (item.dir == Direction.Left)
            {
                if (item.select)
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

        switch (blankOrder)
        {
            case Order.First:
                break;
            case Order.Second:
                break;
            case Order.Third:
                break;
        }

    }



}
