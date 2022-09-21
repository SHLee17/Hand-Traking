using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    enum State
    {
        PlayExample,
        PlayGame,
        EndGame,
        Pause
    }
    enum Phase
    {
        Ready,
        Start,
        End
    }


    [Header("Arrays")]
    [SerializeField]
    List<Pad> clickHistroyList;
    [SerializeField]
    List<PadButton> padButtonList;
    Stack<int> exampleStack;
    Stack<int> playerStack;
    [SerializeField]
    Transform[] buttonHolders;

    [Header("ETC")]
    [SerializeField]
    EventCanvas eventCanvas;
    [SerializeField]
    TMP_Text txtHistory;

    [Header("GameObjects")]
    [SerializeField]
    GameObject objEraser;
    [SerializeField]
    GameObject objCompleted;


    [Header("Data Type")]
    [SerializeField]
    int gameLevel;
    [SerializeField]
    int count;
    float timer;
    float timerReset;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;
    float speed;
    int maxGameLevel;

    void Start()
    {
        foreach (Transform holder in buttonHolders)
        {
            foreach (Transform button in holder)
                padButtonList.Add(button.GetComponent<PadButton>());
        }

        exampleStack = new Stack<int>();
        playerStack = new Stack<int>();
        maxGameLevel = 4;
        timer = timerReset = 1f;
        speed = 1;
        SetLevel();

        state = State.PlayExample;
        phase = Phase.Ready;
    }
    void Update()
    {

        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y-.2f, transform.position.z);


        timer -= Time.deltaTime;

        switch (state)
        {
            case State.PlayExample:
                switch (phase)
                {
                    case Phase.Ready:
                        PadInteractable(false);
                        ChangeState(state, Phase.Start);
                        break;
                    case Phase.Start:
                        if (count > 0)
                        {
                            if (timer < 0)
                            {
                                System.Random rand = new System.Random();

                                int randLine = rand.Next(0, gameLevel);
                                int randButton = rand.Next(0, gameLevel);

                                padButtonList[ConvertArrayIndex(randLine, randButton)].Click(speed);

                                exampleStack.Push(padButtonList[ConvertArrayIndex(randLine, randButton)].Index);

                                timer = timerReset;
                                count -= 1;
                            }
                        }
                        else
                        {
                            if (timer < 0)
                            {
                                eventCanvas.gameObject.SetActive(true);
                                eventCanvas.CountDown();
                                ChangeState(state, Phase.End);
                            }
                        }
                        break;

                    case Phase.End:
                        if (eventCanvas.isEventOver)
                            ChangeState(State.PlayGame, Phase.Ready);
                        break;
                }
                break;
            case State.PlayGame:
                switch (phase)
                {
                    case Phase.Ready:
                        PadInteractable(true);
                        ChangeState(state, Phase.Start);
                        break;
                    case Phase.Start:
                        if (exampleStack.Count > playerStack.Count)
                        {
                            foreach (PadButton item in padButtonList)
                            {
                                if (item.isClick)
                                {
                                    playerStack.Push(item.Index);
                                    clickHistroyList[playerStack.Count - 1].gameObject.SetActive(true);
                                    clickHistroyList[playerStack.Count - 1].txtNumber.text = item.Index.ToString();
                                    item.isClick = false;
                                    break;
                                }
                            }

                            txtHistory.gameObject.SetActive(true);
                            txtHistory.text = exampleStack.Count + " / <color=red>" + playerStack.Count + "</color>";
                            objCompleted.SetActive(false);
                            if(playerStack.Count > 0)
                            objEraser.SetActive(true);
                        }
                        else
                        {
                            txtHistory.text = exampleStack.Count + " / <color=green>" + playerStack.Count + "</color>";
                            objCompleted.SetActive(true);
                        }
                        break;
                    case Phase.End:

                        objEraser.SetActive(false);
                        objCompleted.SetActive(false);
                        txtHistory.gameObject.SetActive(false);

                        ChangeState(State.EndGame, Phase.Ready);
                        break;
                }
                break;
            case State.EndGame:
                switch (phase)
                {
                    case Phase.Ready:

                        for (int i = 0; i < playerStack.Count; i++)
                        {
                            if (playerStack.Pop() != exampleStack.Pop())
                            {
                                eventCanvas.txtWording.text = "<color=red>틀렸습니다.</color>";
                                eventCanvas.gameObject.SetActive(true);
                                goto wrong;
                            }
                        }
                        eventCanvas.txtWording.text = "<color=#006400>맞았습니다..</color>";
                        eventCanvas.gameObject.SetActive(true);

                    wrong:
                        ChangeState(state, Phase.Start);
                        break;
                    case Phase.Start:

                        break;
                    case Phase.End:
                        break;
                }
                break;

            case State.Pause:
                break;
        }


    }
    public void OnInputCompleted()
    {
        ChangeState(State.EndGame, Phase.Ready);
    }
    public void EraserStack()
    {
        if (state != State.EndGame)
        {
            if (playerStack.Count > 0)
            {
                clickHistroyList[playerStack.Count - 1].gameObject.SetActive(false);
                playerStack.Pop();
            }
        }
    }
    void ChangeState(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }
    public void PadInteractable(bool isActive)
    {
        foreach (PadButton button in padButtonList)
        {
            if (button.isActiveAndEnabled)
                button.InteractableActive(isActive);
        }
    }
    public int ConvertArrayIndex(int i, int j)
    {
        return (i * maxGameLevel) + (j % maxGameLevel);
    }
    void SetLevel()
    {

        foreach (Pad item in clickHistroyList)
            item.gameObject.SetActive(false);


        int index = 1;
        for (int i = 0; i < maxGameLevel; i++)
        {
            if (i >= gameLevel) continue;
            for (int j = 0; j < maxGameLevel; j++)
            {
                if (j < gameLevel)
                {
                    padButtonList[ConvertArrayIndex(i, j)].gameObject.SetActive(true);
                    padButtonList[ConvertArrayIndex(i, j)].Index = index;

                    index++;
                }
            }
        }

    }

}
