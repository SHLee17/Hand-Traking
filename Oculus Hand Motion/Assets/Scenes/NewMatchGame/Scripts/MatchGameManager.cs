using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class MatchGameManager : MonoBehaviour
{
    public SubManager subManager;

    [Header("List")]
    [Space(10)]

    //public SubManager subManager;

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
    Vector3 cameraOffset;
    Quaternion cameraRotation;
    float timer, resetTimer;
    int stageCount;
    int score;


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
        subManager.correctNum = 0;
        subManager.clearBonus = 0;
        objResualtCanvas.SetActive(true);
        stageCount = 5;
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

        cameraOffset = new Vector3(-0.3f, 0, 0.4f);
        cameraRotation = new Quaternion(0, 0, 0, 0);
        GameManager.Instance.ResetTimer(gameObject, cameraOffset, cameraRotation);
        objResualtCanvas.SetActive(false);

    }

    void Update()
    {

       
        switch (state)
        {
            case State.GameStart:
                switch (phase)
                {
                    case Phase.Ready:
                        currentStage = stageList[stageRandIndex[currentStageIndex]];
                        SetStage();
                        ChangeState(State.GameStart, Phase.Start);

                        break;
                    case Phase.Start:

                        timer -= Time.deltaTime;
                        progressBar.Set(timer, resetTimer);
                        if (timer < 0)
                        {
                            subManager.seManager.PlaySE(6);
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
                                        else
                                            if (blank.match.isSelect)
                                            return;
                                    }
                                }
                            }
                        }
                        score++;
                        subManager.seManager.PlaySE(1);
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

                }
                break;
        }
    }

    IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1);
        objResualtCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        EndGame();
        currentStageIndex++;

        if (stageList.Count <= currentStageIndex)
        {
            ChangeState(State.EndGame, Phase.End);
            subManager.correctNum = score;
            if (score == stageList.Count)
            {
                subManager.clearBonus = 1000;
                subManager.levelControl.clearStage = true;
            }
            subManager.levelControl.CompleteGame();
            StopCoroutine(NextGame());
        }

        objResualtCanvas.SetActive(false);
        stageCount--;


        if (stageCount > 0)
        {
            ChangeState(State.GameStart, Phase.Ready);
        }
        else
        {
            subManager.correctNum = score;
            if (score == 5)
            {
                subManager.levelControl.clearStage = true;
                subManager.clearBonus = 1000;
                subManager.levelControl.CompleteGame();
            }
            else
                subManager.levelControl.CompleteGame();
        }
    }

    public void SetStage()
    {

        currentStage.gameObject.SetActive(true);
        currentStage.hintActive(true);

        foreach (Angle stage in currentStage.stage)
        {
            foreach (Blank item in stage.angleList)
            {
                if(item.activeMatch == Blank.ActiveMatch.active)
                {
                    item.match = objectPoolQueue.Dequeue();
                    item.match.gameObject.SetActive(true);
                    item.match.PosChange(item.transform);
                    item.match.currentBlank = item;
                    item.match.currentStage = currentStage;
                }
            }
        }

        foreach (Blank item in currentStage.inventoryList)
        {
            if(item.gameObject.activeSelf)
            {
                if (item.activeMatch == Blank.ActiveMatch.active)
                {
                    item.match = objectPoolQueue.Dequeue();
                    item.match.gameObject.SetActive(true);
                    item.match.PosChange(item.transform);
                    item.match.currentBlank = item;
                    item.match.currentStage = currentStage;
                }
            }
        }

        progressBar.gameObject.SetActive(true);
        progressBar.StartProtress();
        timer = resetTimer;
    }


    public void EndGame()
    {
        currentStage.hintActive(false);
        currentStage.gameObject.SetActive(false);

        foreach (Angle stage in currentStage.stage)
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

        foreach (Blank item in currentStage.inventoryList)
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

    public void OnHint()
    {
        currentStage.hintActive(true);
    }
}
