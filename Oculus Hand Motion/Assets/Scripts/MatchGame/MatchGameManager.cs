using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField]
    List<MatchStage> stageList;
    [SerializeField]
    Match tempMatch;

    Queue<Match> matchQueue;

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

        GameObject pool = new GameObject("Pool");
        pool.transform.SetParent(transform);
        for (int i = 0; i < 40; i++)
        {
            Match temp = Instantiate(tempMatch);
            temp.gameObject.SetActive(false);
            temp.transform.SetParent(pool.transform);
            temp.currentStage = stageList[currentStage];
            matchQueue.Enqueue(temp);
        }

        for (int i = 0; i < stageList.Count; i++)
        {
            if (stageList[i].gameObject.activeSelf)
                currentStage = i;
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
                        ChangeState(state, Phase.Start);
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
                            {
                                if (item.isMatchActive) return;
                            }

                        }
                        ChangeState(state, Phase.End);
                        break;
                    case Phase.End:

                        break;
                }
                break;
            case State.EndGame:
                break;
        }
    }

    void SetStage()
    {
        stageList[currentStage].objInfo.SetActive(true);
        foreach (Blank item in stageList[currentStage].blankList)
        {
            if (item.isMatchActive)
            {
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
            if (item.match != null)
            {
                matchQueue.Enqueue(item.match);
                item.match.transform.SetParent(transform);
                item.match = null;
            }
        }

        foreach (Blank item in stageList[currentStage].inventoryList)
        {
            if (item.match != null && item.gameObject.activeSelf)
            {
                matchQueue.Enqueue(item.match);
                item.match.transform.SetParent(transform);
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
