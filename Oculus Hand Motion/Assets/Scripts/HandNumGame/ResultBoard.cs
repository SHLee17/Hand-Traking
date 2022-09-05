using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumGameEnum;

public class ResultBoard : MonoBehaviour
{
    [System.Serializable]
    public class Result
    {
        public Order blankOrder;
        public Sign sign;
        public Dictionary<Order, string> resultDic;
        public int answer;

        public Result(string[] nums, Order blank, Sign sign, int answer)
        {
            resultDic = new Dictionary<Order, string>();

            for (int i = 0; i < nums.Length; i++)
                resultDic.Add((Order)i, nums[i]);

            blankOrder = blank;
            this.sign = sign;
            this.answer = answer;
        }
    }
    [SerializeField]
    Transform resultPartitionParent;

    List<ResultPartition> resultPartitionList;

    public List<Result> resultList;
    public GameObject resultBoardCanvas;

    void Start()
    {
        resultBoardCanvas.gameObject.SetActive(false);

        resultList = new List<Result>();
        resultPartitionList = new List<ResultPartition>();

        foreach (Transform item in resultPartitionParent)
            resultPartitionList.Add(item.GetComponent<ResultPartition>());
    }
    public void CallResultBoard()
    {
        resultBoardCanvas.gameObject.SetActive(true);
        if (resultList.Count > 0)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                resultPartitionList[i].txtExample.text = $"{i + 1}.";
                for (Order j = Order.First; j <= Order.Third; j++)
                {
                    if(j != resultList[i].blankOrder)
                    {
                        resultPartitionList[i].txtExample.text += $" {resultList[i].resultDic[j]} ";
                    }
                    else
                    {
                        if(int.Parse(resultList[i].resultDic[j]) == resultList[i].answer)
                            resultPartitionList[i].txtExample.text += $" <color=green>{resultList[i].resultDic[j]}</color> ";
                        else
                            resultPartitionList[i].txtExample.text += $" <color=red>{resultList[i].resultDic[j]}</color> ";

                    }

                    if (j == Order.First)
                    {
                        switch (resultList[i].sign)
                        {
                            case Sign.Add:
                                resultPartitionList[i].txtExample.text += "+";
                                break;
                            case Sign.Subtract:
                                resultPartitionList[i].txtExample.text += "-";
                                break;
                            case Sign.Divide:
                                resultPartitionList[i].txtExample.text += "¡À";
                                break;
                            case Sign.Multiply:
                                resultPartitionList[i].txtExample.text += "x";
                                break;
                        }
                    }
                    else if (j == Order.Second)
                        resultPartitionList[i].txtExample.text += "=";
                }

                resultPartitionList[i].txtPlayerAnswer.text = resultList[i].answer.ToString();
                resultPartitionList[i].holder.gameObject.SetActive(true);
            }
        }

    }
}
