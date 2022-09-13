using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using NumGameEnum;

namespace NumGameEnum
{
    public enum Order
    {
        First,
        Second,
        Third
    }
    public enum Sign
    {
        Add,
        Subtract,
        Divide,
        Multiply
    }
}

public class NumGameManager : MonoBehaviour
{
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
    Dictionary<Order, Number> answerDict;

    [Header("Texts")]
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
    float resetTimer;
    int exampleCount;
    [SerializeField]
    int answer;
    [SerializeField]
    bool isSetExamQuestions;

    [Header("Class")]
    [SerializeField]
    ResultBoard resultBoard;
    [SerializeField]
    NumberManager numberManager;

    void Start()
    {
        answerDict = new Dictionary<Order, Number>();
        answerDict.Add(Order.First, nums[0]);
        answerDict.Add(Order.Second, nums[1]);
        answerDict.Add(Order.Third, nums[2]);

        exampleCount = 5;
        resetTimer = 5;
        timer = resetTimer;

        foreach (Transform item in fingerPoses)
        {
            foreach (Transform child in item)
                poseList.Add(child.GetComponent<Pose>());
        }

        transform.position = new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.transform.position.y + 0.4f, transform.position.z);
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
        if (exampleCount > 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                txtTimer.text = timer.ToString("N1");

                CheckCount(blankOrder);
            }
            else
            {
                int[] tempArr =
                    { answerDict[Order.First].Num,
                  answerDict[Order.Second].Num,
                  answerDict[Order.Third].Num   };

                string[] result = System.Array.ConvertAll(tempArr, x => x.ToString());

                resultBoard.resultList.Add(new ResultBoard.Result(result, blankOrder, sign, answer));

                exampleCount -= 1;
                timer = resetTimer;
                isSetExamQuestions = false;
            }
            if(exampleCount == 0)
                resultBoard.CallResultBoard();
        }

    }
    void ExampleQuestion(Sign sign, Order blankOrder)
    {
        foreach (KeyValuePair<Order, Number> item in answerDict)
            item.Value.txtNum.color = Color.black;

        void NumSet(Order order, int Num) => answerDict[order].Num = Num;
        int NumGet(Order order) { return answerDict[order].Num; }

        NumSet(blankOrder, 0);

        switch (sign)
        {
            case Sign.Add:
                if (blankOrder == Order.Second)
                {
                    answer = RandRange(2, 10);
                    NumSet(Order.Third, RandRange(answer + 1, 20));
                    NumSet(Order.First, NumGet(Order.Third) - answer);
                }
                else
                {
                    NumSet(Order.First, RandRange(2, 9));
                    answer = RandRange(NumGet(Order.First) + 1, 10);
                    NumSet(Order.Second, answer - NumGet(Order.First));
                }
                break;
            case Sign.Subtract:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.First, RandRange(1, 100));
                    answer = RandRange(2, 10);
                    NumSet(Order.Third, NumGet(Order.First) - answer);
                }
                else
                {
                    answer = RandRange(2, 10);
                    NumSet(Order.First, RandRange(1, 100));
                    NumSet(Order.Second, NumGet(Order.First) - answer);
                }
                break;
            case Sign.Divide:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.Third, RandRange(2, 19));
                    answer = RandRange(2, 10);
                    NumSet(Order.First, NumGet(Order.Third) * answer);

                }
                else
                {
                    answer = RandRange(2, 10);
                    NumSet(Order.Second, RandRange(2, 9));
                    NumSet(Order.First, NumGet(Order.Second) * answer);
                }
                break;
            case Sign.Multiply:
                if (blankOrder == Order.Second)
                {
                    answer = RandRange(2, 10);
                    NumSet(Order.First, RandRange(2, 19));
                    NumSet(Order.Third, answer * NumGet(Order.First));
                }
                else
                {
                    int rand1 = 10, rand2 = 10;

                    while (rand1 * rand2 > 10)
                    {
                        rand1 = RandRange(2, 10);
                        rand2 = RandRange(2, 10);
                    }
                    NumSet(Order.First, rand1);
                    NumSet(Order.Second, rand2);
                    answer = rand1 * rand2;
                }
                break;
        }


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
    void CheckCount(Order blankOrder)
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

        answerDict[blankOrder].txtNum.text = (leftCount + rightCount).ToString();

        answerDict[blankOrder].txtNum.color = Color.green;

        foreach (GameObject item in numberManager.leftHandNumList)
            item.gameObject.SetActive(false);
        foreach (GameObject item in numberManager.rightHandNumList)
            item.gameObject.SetActive(false);

        numberManager.leftHandNumList[leftCount].SetActive(true);
        numberManager.rightHandNumList[rightCount].SetActive(true);

    }



}
