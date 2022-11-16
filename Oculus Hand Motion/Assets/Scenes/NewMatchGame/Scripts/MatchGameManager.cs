using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchGameManager : MonoBehaviour
{
    [Header("List")]
    [Space(10)]

    [SerializeField]
    List<MatchStage> stageList;
    MatchStage currentStage;
    [SerializeField]
    List<int> stageRandIndex;

    [Space(10)]
    [Header("Objects")]
    [Space(10)]

    [SerializeField]
    Match tempMatch;
    [SerializeField]
    GameObject objResualtCanvas;
    [SerializeField]
    TMP_Text txtRight;
    [SerializeField]
    TMP_Text txtTimeOver;
    [SerializeField]
    ProgressBar progressBar;

    Queue<Match> objectPoolQueue;
    GameObject pool;

    [Space(10)]
    [Header("DataType")]
    [Space(10)]

    [SerializeField]
    int currentStageIndex;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;

    float timer, resetTimer;
    

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
        stageRandIndex = new List<int>();

        for (int i = 0; i < stageList.Count; i++)
            stageRandIndex.Add(i);

        int j = 0;
        System.Random rand = new System.Random();
        for (int i = 0; i < stageList.Count; i++)
        {
            while (i == j)
                j = rand.Next(stageList.Count);

            int temp = stageRandIndex[i];
            stageRandIndex[i] = stageRandIndex[j];
            stageRandIndex[j] = temp;
        }

        timer = resetTimer = 60;
        objectPoolQueue = new Queue<Match>();

        pool = new GameObject("Pool");
        pool.transform.SetParent(transform);
        for (int i = 0; i < 40; i++)
        {
            Match temp = Instantiate(tempMatch);
            temp.gameObject.SetActive(false);
            temp.transform.SetParent(pool.transform);
            temp.currentStage = stageList[currentStageIndex];
            objectPoolQueue.Enqueue(temp);
        }

        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f, transform.position.z);
        //transform.LookAt(GameManager.Instance.player.cameraRig.transform);
    }

    void Update()
    {

        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y, transform.position.z);


        switch (state)
        {
            case State.GameStart:
                switch (phase)
                {
                    case Phase.Ready:
                        currentStage = stageList[stageRandIndex[currentStageIndex]];
                        SetStage(currentStage);
                        ChangeState(State.GameStart, Phase.Start);

                        break;
                    case Phase.Start:

                        timer -= Time.deltaTime;
                        progressBar.Set(timer, resetTimer);
                        if (timer < 0)
                        {
                            txtRight.gameObject.SetActive(false);
                            txtTimeOver.gameObject.SetActive(true);
                            ChangeState(State.EndGame, Phase.Ready);

                            return;
                        }
                        foreach (Angle item in currentStage.stage)
                        {
                            foreach (Blank blank in item.angleList)
                            {
                                if (blank.gameObject.activeSelf)
                                {
                                    if (blank.answer == Blank.Answer.Unconditionally)
                                    {
                                        if (blank.match == null)
                                            return;
                                    }
                                }
                            }
                        }
                        txtRight.gameObject.SetActive(true);
                        txtTimeOver.gameObject.SetActive(false);
                        ChangeState(State.EndGame, Phase.Ready);

                        break;
                }
                break;
            case State.EndGame:
                switch (phase)
                {
                    case Phase.Ready:

                        StartCoroutine(NextGame());
                        ChangeState(State.EndGame, Phase.Start);
                        break;
                    case Phase.Start:

                        break;
                }
                break;
        }
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1);
        objResualtCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        EndGame(currentStage);
        currentStageIndex++;

        if (stageList.Count <= currentStageIndex)
        {
            ChangeState(State.EndGame, Phase.End);
            StopCoroutine(NextGame());
        }

        objResualtCanvas.SetActive(false);
        

        ChangeState(State.GameStart, Phase.Ready);


    }

    void SetStage(MatchStage current)
    {
        current.gameObject.SetActive(true);
        current.hintActive(true);

        foreach (Angle stage in current.stage)
        {
            foreach (Blank item in stage.angleList)
            {
                if(item.activeMatch == Blank.ActiveMatch.active)
                {
                    item.match = objectPoolQueue.Dequeue();
                    item.match.gameObject.SetActive(true);
                    item.match.PosChange(item.transform);
                    item.match.currentBlank = item;
                    item.match.currentStage = current;
                }
            }
        }

        foreach (Blank item in current.inventoryList)
        {
            if(item.gameObject.activeSelf)
            {
                if (item.activeMatch == Blank.ActiveMatch.active)
                {
                    item.match = objectPoolQueue.Dequeue();
                    item.match.gameObject.SetActive(true);
                    item.match.PosChange(item.transform);
                    item.match.currentBlank = item;
                    item.match.currentStage = current;
                }
            }
        }

        progressBar.gameObject.SetActive(true);
        progressBar.StartProtress();
        timer = resetTimer;
    }


    void EndGame(MatchStage current)
    {
        current.hintActive(false);
        current.gameObject.SetActive(false);

        foreach (Angle stage in current.stage)
        {
            foreach (Blank item in stage.angleList)
            {
                if (item.match != null)
                {
                    item.match.gameObject.SetActive(false);
                    item.match.currentBlank = null;
                    item.match.currentStage = null;
                    item.match.nextMoveBlank = null;
                    objectPoolQueue.Enqueue(item.match);
                    item.match = null;
                }
            }
        }

        foreach (Blank item in current.inventoryList)
        {
            if (item.gameObject.activeSelf)
            {
                if (item.match != null)
                {
                    item.match.gameObject.SetActive(false);
                    item.match.currentBlank = null;
                    item.match.currentStage = null;
                    item.match.nextMoveBlank = null;
                    objectPoolQueue.Enqueue(item.match);
                    item.match = null;
                }
            }
        }
    }

    void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }
}
