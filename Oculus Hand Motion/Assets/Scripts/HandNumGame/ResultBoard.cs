using NumGameEnum;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    Transform[] resultPageList;

    [SerializeField]
    List<List<ResultPartition>> resultPartitionList;



    [SerializeField]
    TMP_Text txtPage;

    [SerializeField]
    GameObject objResultBoardCanvas;

    [SerializeField]
    GameObject objFinalSelect;

    [SerializeField]
    GameObject objSelectPage;

    public List<Result> resultList;

    int page;

    void Start()
    {
        //foreach (Transform item in transform)
        //    item.gameObject.SetActive(false);

        ResetBoard();

        resultList = new List<Result>();

        resultPartitionList = new List<List<ResultPartition>>();

        foreach (Transform item in resultPageList)
        {
            List<ResultPartition> tempList = new List<ResultPartition>();

            foreach (Transform parent in item)
                tempList.Add(parent.GetComponent<ResultPartition>());

            resultPartitionList.Add(tempList);
        }
    }
    public void ResetBoard()
    {
        resultList.Clear();
        page = 0;
        txtPage.text = $"{page + 1} / 1";
        objResultBoardCanvas.gameObject.SetActive(false);
        objSelectPage.gameObject.SetActive(false);
        objFinalSelect.gameObject.SetActive(false);

        if (resultPartitionList == null) return;
        foreach (var item in resultPartitionList)
        {
            foreach (ResultPartition partition in item)
                partition.holder.SetActive(false);
        }
    }

    public void CallResultBoard()
    {
        objResultBoardCanvas.gameObject.SetActive(true);
        objSelectPage.gameObject.SetActive(true);

        if (resultList.Count > 0)
        {
            for (int i = 0; i < resultList.Count; i++)
            {
                int pageIndex = i / 10;
                int index = i % 10;
                resultPartitionList[pageIndex][index].txtExample.text = $"{i + 1}.  ";
                for (Order j = Order.First; j <= Order.Third; j++)
                {
                    if (j != resultList[i].blankOrder)
                    {
                        resultPartitionList[pageIndex][index].txtExample.text += $" {resultList[i].resultDic[j]} ";
                    }
                    else
                    {
                        if (int.Parse(resultList[i].resultDic[j]) == resultList[i].answer)
                            resultPartitionList[pageIndex][index].txtExample.text += $" <color=#006400>{resultList[i].resultDic[j]}</color> ";
                        else
                            resultPartitionList[pageIndex][index].txtExample.text += $" <color=red>{resultList[i].resultDic[j]}</color> ";

                    }

                    if (j == Order.First)
                    {
                        switch (resultList[i].sign)
                        {
                            case Sign.Add:
                                resultPartitionList[pageIndex][index].txtExample.text += "+";
                                break;
                            case Sign.Subtract:
                                resultPartitionList[pageIndex][index].txtExample.text += "-";
                                break;
                            case Sign.Divide:
                                resultPartitionList[pageIndex][index].txtExample.text += "¡À";
                                break;
                            case Sign.Multiply:
                                resultPartitionList[pageIndex][index].txtExample.text += "x";
                                break;
                        }
                    }
                    else if (j == Order.Second)
                        resultPartitionList[pageIndex][index].txtExample.text += "=";
                }

                resultPartitionList[pageIndex][index].txtPlayerAnswer.text = $"<color=orange>{resultList[i].answer}</color>";
                resultPartitionList[pageIndex][index].holder.SetActive(true);

               
            }
        }
        maxPage = (resultList.Count -1) / 10;

        txtPage.text = $"{page + 1} / {maxPage + 1}";
    }
    int maxPage;
    public void NextPage()
    {

        if (page < maxPage)
            page++;
        else
            return;

        foreach (Transform item in resultPageList)
            item.gameObject.SetActive(false);

        resultPageList[page].gameObject.SetActive(true);
        txtPage.text = $"{page + 1} / {maxPage + 1}";

    }
    public void PreviousPage()
    {

        if (page > 0)
            page--;
        else
            return;

        foreach (Transform item in resultPageList)
            item.gameObject.SetActive(false);

        resultPageList[page].gameObject.SetActive(true);
        txtPage.text = $"{page + 1} / {maxPage + 1}";
    }

    public void OnRestart()
    {
        objFinalSelect.SetActive(true);
        objSelectPage.SetActive(false);
    }

    public void FinalCompletedNo()
    {
        objFinalSelect.SetActive(false);
        objSelectPage.SetActive(true);
    }
}