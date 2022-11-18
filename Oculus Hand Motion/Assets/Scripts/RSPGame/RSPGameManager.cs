using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RSPGameManager : MonoBehaviour
{
    public enum RSP
    {
        Rock,
        Scissor,
        Paper
    }
    enum Result
    {
        Win,
        Draw,
        Lose
    }

    enum State
    {
        SetGame,
        PlayGame,
        EndGame
    }
    enum Phase
    {
        Ready,
        Start,
        End
    }

    [SerializeField]
    Transform poseParent;
    [SerializeField]
    List<Pose> poseList;
    [SerializeField]
    GameObject objResetGame;
    [SerializeField]
    GameObject objHolder;

    [Header("AI")]
    [SerializeField]
    GameObject objRock;
    [SerializeField]
    GameObject objScissor;
    [SerializeField]
    GameObject objPaper;

    [Header("Player")]
    [SerializeField]
    GameObject objPlayerRock;
    [SerializeField]
    GameObject objPlayerScissor;
    [SerializeField]
    GameObject objPlayerPaper;

    [Header("ETC")]
    [SerializeField]
    ProgressBar progressBar;
    [SerializeField]
    TMP_Text txtReset;

    Dictionary<RSP, GameObject> rspDic;
    [SerializeField]
    RSP randRSP;
    [SerializeField]
    RSP playerRSP;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;
    [SerializeField]
    TMP_Text txtInfo;

    System.Random rand = new System.Random();
    bool isReverse;
    float timer;
    float timerReset;
    int stageCount;

    public int gameNum, score, currentGame;

    Vector3 cameraOffset;
    void Start()
    {

        foreach (Transform item in poseParent)
            poseList.Add(item.GetComponent<Pose>());

        rspDic = new Dictionary<RSP, GameObject>();

        rspDic.Add(RSP.Rock, objPlayerRock);
        rspDic.Add(RSP.Scissor, objPlayerScissor);
        rspDic.Add(RSP.Paper, objPlayerPaper);

        timer = timerReset = 6;

        stageCount = 5;


        cameraOffset = new Vector3(0, 0, 0.4f);
        GameManager.Instance.ResetTimer(gameObject, cameraOffset);

    }

    void Update()
    {


        switch (state)
        {
            case State.SetGame:
                objResetGame.SetActive(false);

                SetGame();
                progressBar.StartProtress();
                stageCount--;
                state = State.PlayGame;
                timer = timerReset;
                break;
            case State.PlayGame:
                timer -= Time.deltaTime;
                progressBar.gameObject.SetActive(true);
                progressBar.Set(timer, timerReset);
                foreach (var item in poseList)
                {
                    if (item.select)
                        playerRSP = item.rsp;
                }
                foreach (KeyValuePair<RSP, GameObject> item in rspDic)
                {
                    if (item.Key != playerRSP)
                        item.Value.gameObject.SetActive(false);
                    else
                        item.Value.gameObject.SetActive(true);
                }
                if (timer < timerReset * 0.25f)
                {

                    switch (randRSP)
                    {
                        case RSP.Rock:
                            objRock.gameObject.SetActive(true);
                            break;
                        case RSP.Scissor:
                            objScissor.gameObject.SetActive(true);
                            break;
                        case RSP.Paper:
                            objPaper.gameObject.SetActive(true);
                            break;
                    }
                    if (isReverse)
                    {
                        txtInfo.color = Color.red;
                        txtInfo.text = "지세요.";
                    }
                    else
                    {
                        txtInfo.color = new Color(0, 118f / 255f, 0, 1);

                        txtInfo.text = "이기세요.";
                    }
                }

                if (timer < 0)
                    ChangeState(State.EndGame, Phase.Ready);
                break;

            case State.EndGame:
                switch (phase)
                {
                    case Phase.Ready:

                        Result temp = Judgment(isReverse);
                        txtInfo.fontSize = 60;
                        switch (temp)
                        {
                            case Result.Win:
                                score++;
                                txtInfo.color = new Color(0, 118f / 255f, 0, 1);
                                break;
                            case Result.Draw:
                                txtInfo.color = Color.gray;
                                break;
                            case Result.Lose:
                                txtInfo.color = Color.red;
                                break;
                        }
                        txtInfo.text = temp.ToString();
                        timer = timerReset / 2;
                        progressBar.StartProtress();
                        currentGame++;
                        if (gameNum<=currentGame)
                            phase = Phase.End;
                        else
                            phase = Phase.Start;
                        break;
                    case Phase.Start:
                        
                       timer -= Time.deltaTime;
                        //progressBar.gameObject.SetActive(true);

                        //progressBar.Set(timer, timerReset / 2);

                        if (timer < timerReset / 4)
                        {
                            txtInfo.color = Color.black;

                            txtInfo.fontSize = 30;
                            txtInfo.text = "바로 다음게임이 시작됩니다.";
                        }
                        if (stageCount <= 0)
                        {
                            txtInfo.text = "";

                            txtReset.text = $"총 <color=green>{score}</color> 번 이기셨습니다.";

                            objResetGame.SetActive(true);
                            objHolder.SetActive(false);
                        }
                        else
                        {
                            if (timer < 0) state = State.SetGame;
                        }
                        break;
                    case Phase.End:
                      
                        break;
                }
                break;
        }

    }

    void SetGame()
    {
        txtInfo.text = "";
        randRSP = GameManager.Instance.RandomEnum<RSP>();

        objPaper.gameObject.SetActive(false);
        objRock.gameObject.SetActive(false);
        objScissor.gameObject.SetActive(false);

        isReverse = rand.NextDouble() >= 0.5f;
    }

    Result Judgment(bool isReverse)
    {
        switch (playerRSP)
        {
            case RSP.Rock:
                if (isReverse)
                {
                    switch (randRSP)
                    {
                        case RSP.Scissor:
                            return Result.Lose;
                        case RSP.Paper:
                            return Result.Win;
                    }
                }
                else
                {
                    switch (randRSP)
                    {
                        case RSP.Scissor:
                            return Result.Win;
                        case RSP.Paper:
                            return Result.Lose;
                    }
                }
                break;
            case RSP.Scissor:
                if (isReverse)
                {
                    switch (randRSP)
                    {
                        case RSP.Rock:
                            return Result.Win;
                        case RSP.Paper:
                            return Result.Lose;
                    }
                }
                else
                {
                    switch (randRSP)
                    {
                        case RSP.Rock:
                            return Result.Lose;
                        case RSP.Paper:
                            return Result.Win;
                    }
                }
                break;
            case RSP.Paper:
                if (isReverse)
                {
                    switch (randRSP)
                    {
                        case RSP.Rock:
                            return Result.Lose;
                        case RSP.Scissor:
                            return Result.Win;
                    }
                }
                else
                {
                    switch (randRSP)
                    {
                        case RSP.Rock:
                            return Result.Win;
                        case RSP.Scissor:
                            return Result.Lose;
                    }
                }
                break;
        }

        return Result.Draw;
    }

    void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }

    public void OnResetGameButton(bool isTrue)
    {
        if (isTrue) 
        {
            objHolder.SetActive(true);
            stageCount = 5;
            state = State.SetGame;
        }

        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
