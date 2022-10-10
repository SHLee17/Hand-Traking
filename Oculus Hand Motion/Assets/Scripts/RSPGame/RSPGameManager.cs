using System.Collections.Generic;
using TMPro;
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
    void Start()
    {
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
                        txtInfo.text = Judgment(isReverse).ToString();
                        timer = timerReset / 2;
                        progressBar.StartProtress();
                        phase = Phase.Start;
                        break;
                    case Phase.Start:
                        timer -= Time.deltaTime;
                        progressBar.gameObject.SetActive(true);

                        progressBar.Set(timer, timerReset / 2);

                        if (timer < timerReset / 4) txtInfo.text = "바로 다음게임이 시작됩니다.";
                        if (timer < 0) state = State.SetGame;
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
}
