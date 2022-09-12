using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    enum State
    {
        PlayExample,
        PlayGame,
        Pause
    }
    enum Phase
    {
        Ready,
        Start,
        End
    }


    public int gameLevel;


    [Header("Arrays")]
    [SerializeField]
    List<Pad> clickHistroyList;
    [SerializeField]
    List<PadButton> padButtonList;

    Stack<int> exampleStack;
    Stack<int> playerStack;

    [SerializeField]
    GameObject[] objPanel;


    [Header("ETC")]
    [SerializeField]
    EventCanvas eventCanvas;
    int count;
    float timer;
    float timerReset;
    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;

    void Start()
    {
        exampleStack = new Stack<int>();
        playerStack = new Stack<int>();

        gameLevel = 3;
        timer = timerReset = 1.5f;
        count = 7;

        SetLevel();

        state = State.PlayExample;
        phase = Phase.Ready;

    }
    void Update()
    {
        timer -= Time.deltaTime;

        GameManager.Instance.player.cameraRig.transform.LookAt(transform);

        switch (state)
        {
            case State.PlayExample:
                switch (phase)
                {
                    case Phase.Ready:
                        PadInteractle(false);
                        StateChange(state, Phase.Start);
                        break;
                    case Phase.Start:
                        if (count > 0)
                        {
                            if (timer < 0)
                            {
                                System.Random rand = new System.Random();

                                int randLine = rand.Next(0, gameLevel);
                                int randButton = rand.Next(0, gameLevel);

                                padButtonList[ConvertArrayIndex(randLine, randButton)].Click();

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
                                StateChange(state, Phase.End);
                            }
                        }
                        break;

                    case Phase.End:
                        if (eventCanvas.isEventOver)
                            StateChange(State.PlayGame, Phase.Ready);
                        break;
                }
                break;
            case State.PlayGame:
                switch (phase)
                {
                    case Phase.Ready:
                        PadInteractle(true);
                        StateChange(state, Phase.Start);
                        break;
                    case Phase.Start:
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

                        break;
                    case Phase.End:
                        break;
                }
                break;
            case State.Pause:
                break;
        }


    }
    void StateChange(State state, Phase phase)
    {
        this.state = state;
        this.phase = phase;
    }
    public void PadInteractle(bool isActive)
    {
        foreach (PadButton button in padButtonList)
        {
            if (button.isActiveAndEnabled)
                button.InteractableActive(isActive);
        }
    }
    public int ConvertArrayIndex(int i, int j)
    {
        return (i * 5) + (j % 5);
    }
    void SetLevel()
    {
        objPanel[gameLevel - 3].SetActive(true);

        foreach (Pad item in clickHistroyList)
            item.gameObject.SetActive(false);


        int index = 1;
        for (int i = 0; i < 5; i++)
        {
            if (i >= gameLevel) continue;
            for (int j = 0; j < 5; j++)
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
