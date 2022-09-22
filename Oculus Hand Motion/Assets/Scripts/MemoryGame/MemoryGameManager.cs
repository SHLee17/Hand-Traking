using MemoryGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MemoryGame
{
    public enum PadColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Orange,
        Purple
    }


}

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
    enum Type
    {
        Number,
        Color,
        Typing,
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
    [SerializeField]
    MeshRenderer exampleColorMeshRenderer;
    [SerializeField]
    TMP_Text txtExampleColor;

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
    float speed;
    int maxGameLevel;
    int maxCount;

    [SerializeField]
    State state;
    [SerializeField]
    Phase phase;
    [SerializeField]
    Type type;

    PadColor randColor;

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
        maxCount = count;
        timer = timerReset = 1f;
        speed = 1;

        state = State.PlayExample;
        phase = Phase.Ready;
    }
    void Update()
    {

        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f, transform.position.z);

        switch (state)
        {
            case State.PlayExample:
                switch (phase)
                {
                    case Phase.Ready:
                        SetLevel(type);
                        PadInteractable(false);
                        ChangeState(state, Phase.Start);
                        if (type == Type.Color)
                            StartCoroutine(ShowExampleColor(2));
                        break;
                    case Phase.Start:

                        PlayExample(type);

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

                        PlayGame(type);

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
                        eventCanvas.txtWording.text = "<color=#006400>맞았습니다.</color>";
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
    void SetLevel(Type type)
    {

        txtHistory.text = $"<color=red>{exampleStack.Count}</color> / {count}";
        foreach (Pad item in clickHistroyList)
            item.gameObject.SetActive(false);

        switch (type)
        {
            case Type.Number:

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
                break;
            case Type.Color:

                SetColorButton(ConvertArrayIndex(0, 0), true, PadColor.Red, 1);
                SetColorButton(ConvertArrayIndex(0, 3), true, PadColor.Blue, 2);
                SetColorButton(ConvertArrayIndex(1, 0), true, PadColor.Green, 3);
                SetColorButton(ConvertArrayIndex(1, 3), true, PadColor.Yellow, 4);
                SetColorButton(ConvertArrayIndex(0, 1), true, PadColor.Orange, 5);
                SetColorButton(ConvertArrayIndex(0, 2), true, PadColor.Purple, 6);


                break;
            case Type.Typing:
                break;
        }


    }
    void SetColorButton(int buttonIndex, bool isActive, PadColor padColor, int index)
    {
        padButtonList[buttonIndex].gameObject.SetActive(isActive);
        padButtonList[buttonIndex].Index = index;
        padButtonList[buttonIndex].PadColor = padColor;
    }
    void PlayExample(Type type)
    {
        timer -= Time.deltaTime;

        switch (type)
        {

            case Type.Number:
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

                        txtHistory.text = $"<color=red>{exampleStack.Count}</color> / {maxCount}";
                    }
                }
                else
                {
                    txtHistory.text = $"<color=green>{exampleStack.Count}</color> / {maxCount}";
                    if (timer < 0)
                    {
                        eventCanvas.gameObject.SetActive(true);
                        eventCanvas.CountDown();
                        ChangeState(state, Phase.End);
                    }
                }
                break;
            case Type.Color:
                break;
            case Type.Typing:

                break;
        }
    }

    void PlayGame(Type type)
    {
        if (playerStack.Count > 0)
            objEraser.SetActive(true);
        else
            objEraser.SetActive(false);

        if (exampleStack.Count > playerStack.Count)
        {
            txtHistory.gameObject.SetActive(true);
            txtHistory.text = $"<color=red>{playerStack.Count}</color> / {exampleStack.Count}";
            objCompleted.SetActive(false);
        }
        else
        {
            txtHistory.text = $"<color=green>{playerStack.Count}</color> / {exampleStack.Count}";
            objCompleted.SetActive(true);
        }

        switch (type)
        {
            case Type.Number:
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
                }
                break;
            case Type.Color:

                break;
            case Type.Typing:

                break;
        }
    }

    IEnumerator ShowExampleColor(float speed)
    {
        WaitForSeconds wfs = new WaitForSeconds(speed);
        WaitForSeconds wfs2 = new WaitForSeconds(0.1f);

        for (int i = 0; i < count; i++)
        {

            System.Random rand = new System.Random();

            int randIndex = rand.Next(1, 7);

            exampleStack.Push(randIndex);

            foreach (PadButton item in padButtonList)
            {
                if (item.gameObject.activeSelf)
                {
                    if (randIndex == item.Index)
                    {
                        exampleColorMeshRenderer.material.color = item.colorVisual.NormalColor;
                        txtExampleColor.text = item.PadColor.ToString();
                        txtExampleColor.color = item.colorVisual.NormalColor;
                    }

                }
            }
            yield return wfs;

            exampleColorMeshRenderer.material.color = Color.white;
            txtExampleColor.text = "";

            yield return wfs2;

        }
    }
}
