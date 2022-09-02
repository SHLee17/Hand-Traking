using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBoard : MonoBehaviour
{
    [System.Serializable]
    public class Result
    {
        public NumGameManager.Order blankOrder;
        public NumGameManager.Sign sign;
        public Dictionary<NumGameManager.Order, string> resultDic;
        public int playerAnswer;

        public Result(string[] nums, NumGameManager.Order blank, NumGameManager.Sign sign, int playerAnswer)
        {
            resultDic = new Dictionary<NumGameManager.Order, string>();

            for (int i = 0; i < nums.Length; i++)
                resultDic.Add((NumGameManager.Order)i, nums[i]);

            blankOrder = blank;
            this.sign = sign;
            this.playerAnswer = playerAnswer;
        }
    }


    [SerializeField]
    Transform resultPartitionParent;

    List<ResultPartition> resultPartitionList;

    public List<Result> resultList;

    void Start()
    {
        resultList = new List<Result>();

        foreach (Transform item in resultPartitionParent)
            resultPartitionList.Add(item.GetComponent<ResultPartition>());
    }
    public void CallResultBoard()
    {
        
        if(resultList.Count > 0)
        {

        }

    }
}
