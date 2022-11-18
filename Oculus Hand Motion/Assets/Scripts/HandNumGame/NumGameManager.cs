using NumGameEnum;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    public enum State
    {
        LevelSelect,
        PlayGame,
        Result
    }
    public enum Phase
    {
        Ready,
        Start,
        End
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
    //[SerializeField]
    //TMP_Text txtTimer;
    [SerializeField]
    TMP_Text txtSign;
    [SerializeField]
    TMP_Text txtExampleCount;

    [Header("Enums")]
    [SerializeField]
    Sign sign;
    [SerializeField]
    Order blankOrder;

    [Header("Data Type")]
    bool isToturial;
    int leftCount, rightCount;
    float timer;
    float resetTimer;
    int exampleCount;
    int level;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;
    [SerializeField]
    int answer;
    public Vector3 cameraOffset;

    [Header("Class")]
    [SerializeField]
    ResultBoard resultBoard;
    [SerializeField]
    NumberManager numberManager;
    [SerializeField]
    NumGameOption numGameOption;
    [SerializeField]
    PokeInteractable CheckButtonInteractable;
    [SerializeField]
    ProgressBar progressBar;

    [Header("GameObjects")]
    [SerializeField]
    GameObject objMainGame;
    [SerializeField]
    GameObject objCheckButton;
    [SerializeField]
    GameObject objWrongAnswer;
    [SerializeField]
    GameObject objRightAnswer;
    [SerializeField]
    GameObject objToturialClick;
    [SerializeField]
    GameObject objToturialClickCanvas;
    [SerializeField]
    GameObject objToturialPush;

    Color green = new Color(0, 100, 0);

    void Start()
    {
        

        isToturial = true;
        answerDict = new Dictionary<Order, Number>();
        answerDict.Add(Order.First, nums[0]);
        answerDict.Add(Order.Second, nums[1]);
        answerDict.Add(Order.Third, nums[2]);

        //exampleCount = 5;
        resetTimer = 20;
        timer = resetTimer;

        foreach (Transform item in fingerPoses)
        {
            foreach (Transform child in item)
                poseList.Add(child.GetComponent<Pose>());
        }

        cameraOffset = new Vector3(-0.1f, 0.2f, 0.8f);
        GameManager.Instance.ResetTimer(gameObject, cameraOffset);

    }

    void Update()
    {

        switch (state)
        {

            case State.LevelSelect:
                switch (phase)
                {
                    case Phase.Ready:
                        objMainGame.SetActive(false);
                        numberManager.gameObject.SetActive(true);
                        progressBar.gameObject.SetActive(false);
                        txtExampleCount.text = $"<color=red>0</color> / {exampleCount}";

                        if (isToturial)
                        {
                            //objToturialClick.SetActive(true);
                            objToturialClickCanvas.SetActive(true);
                        }
                        else
                        {
                            objToturialClick.SetActive(false);
                            objToturialClickCanvas.SetActive(false);
                        }
                        ChangeState(state, Phase.Start);
                        break;
                    case Phase.Start:

                        if (numGameOption.isOptionCompleted)
                        {
                            numGameOption.isOptionCompleted = false;
                            numGameOption.gameObject.SetActive(false);
                            objMainGame.SetActive(true);
                            numGameOption.isOptionCompleted = false;
                            objCheckButton.SetActive(true);

                            exampleCount = numGameOption.exampleMaxCount.Value;
                            level = numGameOption.levelSelect.Value;

                            txtExampleCount.text = $"<color=red>0</color> / {exampleCount}";

                            ChangeState(State.PlayGame, Phase.Ready);
                        }
                        break;
                }
                break;
            case State.PlayGame:
                switch (phase)
                {
                    case Phase.Ready:
                        StartCoroutine(CheckButtonEnable());
                        timer = resetTimer;
                        blankOrder = GameManager.Instance.RandomEnum<Order>(1);
                        sign = GameManager.Instance.RandomEnum<Sign>();
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
                        ChangeState(state, Phase.Start);
                        progressBar.StartProtress();
                        progressBar.gameObject.SetActive(true);
                        break;
                    case Phase.Start:
                        if (exampleCount <= 0)
                            ChangeState(state, Phase.End);

                        if (isToturial)
                        {
                            CheckCount(blankOrder);
                            objToturialPush.SetActive(true);
                        }
                        else
                        {
                            objToturialPush.SetActive(false);
                            if (timer > 0)
                            {
                                timer -= Time.deltaTime;
                                progressBar.Set(timer, resetTimer);
                                //txtTimer.text = timer.ToString("N1");

                                CheckCount(blankOrder);
                            }
                            else
                            {
                                CheckAnswer();
                                ChangeState(State.PlayGame, Phase.End);
                            }
                        }
                        break;
                    case Phase.End:
                        if (exampleCount <= 0)
                        {
                            txtExampleCount.text = $"<color=#006400>{numGameOption.exampleMaxCount.Value}</color> / {numGameOption.exampleMaxCount.Value}";
                            ChangeState(State.Result, Phase.Ready);
                        }
                        else
                        {
                            txtExampleCount.text = $"<color=red>{numGameOption.exampleMaxCount.Value - exampleCount}</color> / {numGameOption.exampleMaxCount.Value}";
                            ChangeState(state, Phase.Ready);
                        }
                        break;
                }
                break;
            case State.Result:
                switch (phase)
                {
                    case Phase.Ready:
                        progressBar.gameObject.SetActive(false);
                        objCheckButton.gameObject.SetActive(false);
                        numberManager.gameObject.SetActive(false);
                        resultBoard.CallResultBoard();
                        ChangeState(state, Phase.End);
                        break;
                    case Phase.End:
                        break;
                }
                break;
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
                    do
                    {
                        NumSet(Order.First, RandRange(1, 100));
                        answer = RandRange(2, 10);
                        NumSet(Order.Third, NumGet(Order.First) - answer);
                    }
                    while (NumGet(Order.Third) <= 0);
                }
                else
                {
                    do
                    {
                        answer = RandRange(2, 10);
                        NumSet(Order.First, RandRange(1, 100));
                        NumSet(Order.Second, NumGet(Order.First) - answer);
                    }
                    while (NumGet(Order.Second) <= 0);
                }
                break;
            case Sign.Divide:
                if (blankOrder == Order.Second)
                {
                    NumSet(Order.Third, RandRange(2, level));
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
                    NumSet(Order.First, RandRange(2, level));
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
    IEnumerator CheckButtonEnable()
    {
        CheckButtonInteractable.enabled = false;
        yield return new WaitForSeconds(0.5f);
        CheckButtonInteractable.enabled = true;
    }
    private void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
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

        answerDict[blankOrder].Num = (leftCount + rightCount);

        answerDict[blankOrder].txtNum.color = green;

        foreach (GameObject item in numberManager.leftHandNumList)
            item.gameObject.SetActive(false);
        foreach (GameObject item in numberManager.rightHandNumList)
            item.gameObject.SetActive(false);

        numberManager.leftHandNumList[leftCount].SetActive(true);
        numberManager.rightHandNumList[rightCount].SetActive(true);

    }
    public void CheckAnswer()
    {

        int[] tempArr =
                               {   answerDict[Order.First].Num,
                                    answerDict[Order.Second].Num,
                                    answerDict[Order.Third].Num   };

        string[] result = System.Array.ConvertAll(tempArr, x => x.ToString());

        resultBoard.resultList.Add(new ResultBoard.Result(result, blankOrder, sign, answer));

        if (isToturial) return;
        exampleCount -= 1;
        timer = resetTimer;

        
    }

    public void Puase(bool isBool)
    {
        if (isBool)
        {
            Time.timeScale = 0;

            if (answerDict[blankOrder].Num == answer)
            {
                objRightAnswer.SetActive(true);
                GameManager.Instance.AddTotal(1);
            }
            else
                objWrongAnswer.SetActive(true);
        }
        else
        {
            objRightAnswer.SetActive(false);
            objWrongAnswer.SetActive(false);
            StartCoroutine(CheckButtonEnable());
            Time.timeScale = 1;

            if (isToturial)
            {
                resultBoard.resultList.Clear();
                isToturial = false;
            }
            ChangeState(State.PlayGame, Phase.End);
        }
    }

    public void ResetGame()
    {
        numGameOption.gameObject.SetActive(true);
        ChangeState(State.LevelSelect, Phase.Ready);
        numGameOption.ResetObject();
        resultBoard.ResetBoard();
    }


    public void LobbyScene()
    {

        SceneManager.LoadScene(0);
    }
}
