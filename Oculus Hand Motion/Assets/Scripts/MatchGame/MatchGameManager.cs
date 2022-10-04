using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField]
    List<MatchStage> stageList;
    [SerializeField]
    Match tempMatch;

    Queue<Match> matchQueue;

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
        currentStage = 0;
        matchQueue = new Queue<Match>();

        GameObject pool = new GameObject("Pool");
        pool.transform.SetParent(transform);
        for (int i = 0; i < 40; i++)
        {
            Match temp = Instantiate(tempMatch);
            temp.gameObject.SetActive(false);
            temp.transform.SetParent(pool.transform);
            matchQueue.Enqueue(temp);
        }

    }

    void Update()
    {
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
                            if (item.isRightAnswer)
                            {
                                if (!item.isMatchActive) return;
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
        foreach (Blank item in stageList[currentStage].blankList)
        {
            if (item.isMatchActive)
            {
                item.match = matchQueue.Dequeue();
                item.match.gameObject.SetActive(true);
                item.match.transform.position = item.transform.position;
                item.match.transform.rotation = item.transform.rotation;
                item.match.currentBlank = item;
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
    }

    void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }
}
