using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField]
    List<MatchStage> stageList;
    [SerializeField]
    Match tempMatch;
    [SerializeField]
    GameObject objRightAnswerCanvas;

    Queue<Match> matchQueue;

    GameObject pool;

    [SerializeField]
    int currentStage;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;
    enum State
    {
        GameStart,
        EndGame
    }
    enum Phase
    {
        Ready,
        Start,
        End
    }
    void Start()
    {
        matchQueue = new Queue<Match>();

        pool = new GameObject("Pool");
        pool.transform.SetParent(transform);
        for (int i = 0; i < 40; i++)
        {
            Match temp = Instantiate(tempMatch);
            temp.gameObject.SetActive(false);
            temp.transform.SetParent(pool.transform);
            temp.currentStage = stageList[currentStage];
            matchQueue.Enqueue(temp);
        }

    }

    void Update()
    {
        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f, transform.position.z);


        switch (state)
        {
            case State.GameStart:
                switch (phase)
                {
                    case Phase.Ready:
                        SetStage();
                        ChangeState(State.GameStart, Phase.Start);
                        break;
                    case Phase.Start:
                        foreach (Blank item in stageList[currentStage].blankList)
                        {
                            Blank.Pair temp = Blank.Pair.Unconditionally;
                            if(item.pair != Blank.Pair.None &&
                                item.pair != Blank.Pair.Unconditionally)
                                temp = item.pair;
                            
                            if (item.isRightAnswer)
                            {
                                if (!item.isMatchActive) return;
                                if (temp != item.pair) return;
                            }
                            else
                                if (item.isMatchActive) return;

                        }
                        ChangeState(State.GameStart, Phase.End);
                        break;
                    case Phase.End:
                        objRightAnswerCanvas.SetActive(true);
                        ChangeState(State.EndGame, Phase.Ready);
                        break;
                }
                break;
            case State.EndGame:
                switch (phase)
                {
                    case Phase.Ready:
                        //stageList[currentStage].gameObject.SetActive(false);
                        foreach (var item in stageList)
                            item.gameObject.SetActive(false);
                        pool.gameObject.SetActive(false);
                        ChangeState(State.EndGame, Phase.Start);
                        //StartCoroutine(NextGame());
                        break;

                }
                break;
        }
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2);
        objRightAnswerCanvas.SetActive(false);
        yield return new WaitForSeconds(2);
        EndStage();
        ChangeState(State.GameStart, Phase.Ready);
        currentStage++;
        if (stageList.Count == currentStage)
            currentStage = 0;

    }

    void SetStage()
    {
        objRightAnswerCanvas.SetActive(false);
        stageList[currentStage].gameObject.SetActive(true);
        stageList[currentStage].objInfo.SetActive(true);
        foreach (Blank item in stageList[currentStage].blankList)
        {
            if (item.isComebackMatch)
            {
                item.isMatchActive = true;
                item.match = matchQueue.Dequeue();
                item.match.gameObject.SetActive(true);
                item.match.PosChange(item.transform);
                item.match.currentBlank = item;
                item.match.currentStage = stageList[currentStage];

                if (item.pair != Blank.Pair.None)
                    item.isRightAnswer = true;
                else
                    item.isRightAnswer = false;
            }
            else
            {
                if (item.pair != Blank.Pair.None)
                    item.isRightAnswer = true;
                else
                    item.isRightAnswer = false;
                item.isMatchActive = false;
            }
        }
        foreach (Blank item in stageList[currentStage].inventoryList)
        {
            
            if (item.isMatchActive && item.gameObject.activeSelf)
            {
                item.match = matchQueue.Dequeue();
                item.match.gameObject.SetActive(true);
                item.match.PosChange(item.transform);
                item.match.currentBlank = item;
                item.match.currentStage = stageList[currentStage];
            }
        }
    }

    void EndStage()
    {
        foreach (Blank item in stageList[currentStage].blankList)
        {
            item.isMatchActive = false;

            if (item.match != null)
            {
                matchQueue.Enqueue(item.match);
                item.match.transform.SetParent(transform);
                item.match.PosChange(transform);
                item.match.gameObject.SetActive(false);
                item.match = null;
            }
        }

        foreach (Blank item in stageList[currentStage].inventoryList)
        {
            item.isMatchActive = false;

            if (item.match != null)
            {
                matchQueue.Enqueue(item.match);
                item.match.transform.SetParent(transform);
                item.match.PosChange(transform);
                item.match.gameObject.SetActive(false);
                item.match = null;
            }
        }
    }

    void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }
}
