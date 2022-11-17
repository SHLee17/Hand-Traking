using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

    public SubManager subManager;

    [SerializeField]
    Transform poseParent;
    [SerializeField]
    List<Pose> poseList;

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

    public int gameNum, score, currentGame;

    void Start()
    {

        subManager.correctNum = 0;
        subManager.clearBonus = 0;

        foreach (Transform item in poseParent)
            poseList.Add(item.GetComponent<Pose>());

        rspDic = new Dictionary<RSP, GameObject>();

        rspDic.Add(RSP.Rock, objPlayerRock);
        rspDic.Add(RSP.Scissor, objPlayerScissor);
        rspDic.Add(RSP.Paper, objPlayerPaper);

        timer = timerReset = 10;


    }

    void Update()
    {

        transform.position =
            new Vector3(transform.position.x,
            GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f,
            transform.position.z);

        switch (state)
        {
            case State.SetGame:
                SetGame();
                progressBar.StartProtress();
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
                if (timer < timerReset * 0.5f)
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
                        txtInfo.text = "지세요.";
                    else
                        txtInfo.text = "이기세요.";
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
                                subManager.seManager.PlaySE(1);
                                score++;
                                txtInfo.color = new Color(0, 118f / 255f, 0, 1);
                                break;
                            case Result.Draw:
                                subManager.seManager.PlaySE(5);
                                txtInfo.color = Color.gray;
                                break;
                            case Result.Lose:
                                subManager.seManager.PlaySE(6);
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
                        progressBar.gameObject.SetActive(true);

                        progressBar.Set(timer, timerReset / 2);

                        if (timer < timerReset / 4)
                        {
                            txtInfo.color = Color.black;

                            txtInfo.fontSize = 30;
                            txtInfo.text = "바로 다음게임이 시작됩니다.";
                        }
                        if (timer < 0) state = State.SetGame;
                        break;
                    case Phase.End:
                        subManager.correctNum = score;
                        if (score >= gameNum)
                        {
                            subManager.levelControl.clearStage = true;
                            subManager.clearBonus = 1000;
                            subManager.levelControl.CompleteGame();
                        }
                        else
                            subManager.levelControl.CompleteGame();
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
}
