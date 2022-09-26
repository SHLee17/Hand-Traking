using MemoryGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MemoryGame
{
    public enum PadColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Orange,
        Purple,
        White
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
    Dictionary<Type, int> gameLevelDict;

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
    [SerializeField]
    GameObject objColorExample;

    [Header("Data Type")]
    [SerializeField]
    int gameLevel;
    [SerializeField]
    int count;
    [SerializeField]
    ProgressBar progressBar;

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


    void Start()
    {
        foreach (Transform holder in buttonHolders)
        {
            foreach (Transform button in holder)
                padButtonList.Add(button.GetComponent<PadButton>());
        }

        gameLevelDict = new Dictionary<Type, int>();
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
                        break;
                    case Phase.Start:
                        timer -= Time.deltaTime;

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
                        timer = timerReset = 30f;
                        ChangeState(state, Phase.Start);

                        break;
                    case Phase.Start:
                        timer -= Time.deltaTime;
                        progressBar.gameObject.SetActive(true);

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
                        progressBar.Init(timer, timerReset);

                        PlayGame(type);

                        if (timer < 0)
                            ChangeState(state, Phase.End);

                        if (count > 0)
                            ChangeState(state, Phase.Ready);

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
        progressBar.gameObject.SetActive(false);

        txtHistory.text = $"<color=red>{exampleStack.Count}</color> / {count}";
        foreach (Pad item in clickHistroyList)
            item.gameObject.SetActive(false);

        foreach (PadButton item in padButtonList)
        {
            item.gameObject.SetActive(false);
            item.PadColor = PadColor.White;
        }

        objColorExample.SetActive(false);
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
                            padButtonList[ConvertArrayIndex(i, j)].txtName.text = index.ToString();

                            index++;
                        }
                    }
                }
                break;

            case Type.Color:
                objColorExample.SetActive(true);
                SetColorButton(ConvertArrayIndex(0, 0), true, PadColor.Red, 1);
                SetColorButton(ConvertArrayIndex(0, 3), true, PadColor.Blue, 2);
                SetColorButton(ConvertArrayIndex(1, 0), true, PadColor.Green, 3);
                SetColorButton(ConvertArrayIndex(1, 3), true, PadColor.Yellow, 4);
                SetColorButton(ConvertArrayIndex(0, 1), true, PadColor.Orange, 5);
                SetColorButton(ConvertArrayIndex(0, 2), true, PadColor.Purple, 6);
                StartCoroutine(ShowExampleColor(2));
                break;

            case Type.Typing:

                padButtonList[0].txtName.text = "A";
                padButtonList[1].txtName.text = "S";
                padButtonList[2].txtName.text = "D";
                padButtonList[3].txtName.text = "F";
                padButtonList[4].txtName.text = "Q";
                padButtonList[5].txtName.text = "W";
                padButtonList[6].txtName.text = "E";
                padButtonList[7].txtName.text = "R";

                for (int i = 0; i < 8; i++)
                {
                    padButtonList[i].gameObject.SetActive(true);
                    padButtonList[i].Index = i + 1;
                }

                System.Random rand = new System.Random();


                for (int i = 0; i < 5; i++)
                {
                    int randIndex = rand.Next(1, 8);
                    clickHistroyList[i].txtName.text = padButtonList[randIndex].txtName.text;
                    clickHistroyList[i].gameObject.SetActive(true);
                }

                timer = timerReset = 10f;
                break;
        }


    }
    void SetColorButton(int buttonIndex, bool isActive, PadColor padColor, int index)
    {
        padButtonList[buttonIndex].gameObject.SetActive(isActive);
        padButtonList[buttonIndex].Index = index;
        padButtonList[buttonIndex].PadColor = padColor;
        padButtonList[buttonIndex].txtName.text = padColor.ToString();
    }
    void PlayExample(Type type)
    {

        System.Random rand = new System.Random();
        switch (type)
        {
            case Type.Number:
                if (count > 0)
                {
                    if (timer < 0)
                    {
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
                        CallEventCanvas();
                }
                break;
            case Type.Color:
                break;
            case Type.Typing:
                progressBar.gameObject.SetActive(true);
                progressBar.Init(timer, timerReset);

                if (timer < 0)
                    CallEventCanvas();
                break;
        }
    }

    void PlayGame(Type type)
    {

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
                            clickHistroyList[playerStack.Count - 1].image.sprite = clickHistroyList[playerStack.Count - 1].sprites[0];
                            clickHistroyList[playerStack.Count - 1].gameObject.SetActive(true);
                            clickHistroyList[playerStack.Count - 1].txtName.text = item.Index.ToString();
                            item.isClick = false;
                            break;
                        }
                    }
                }
                break;
            case Type.Color:

                if(exampleStack.Count > playerStack.Count)
                {
                    foreach (PadButton item in padButtonList)
                    {
                        if (item.isClick)
                        {
                            playerStack.Push(item.Index);
                            clickHistroyList[playerStack.Count - 1].image.sprite = clickHistroyList[playerStack.Count - 1].sprites[1];
                            clickHistroyList[playerStack.Count - 1].gameObject.SetActive(true);
                            clickHistroyList[playerStack.Count - 1].txtName.text = item.PadColor.ToString();
                            item.isClick = false;
                            break;
                        }
                    }
                }

                break;
            case Type.Typing:
                if (exampleStack.Count > playerStack.Count)
                {
                    foreach (PadButton item in padButtonList)
                    {
                        if (item.isClick)
                        {
                            playerStack.Push(item.Index);
                            clickHistroyList[playerStack.Count - 1].image.sprite = clickHistroyList[playerStack.Count - 1].sprites[0];
                            clickHistroyList[playerStack.Count - 1].gameObject.SetActive(true);
                            clickHistroyList[playerStack.Count - 1].txtName.text = item.ButtonKey;
                            item.isClick = false;
                            break;
                        }
                    }
                }
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
        CallEventCanvas();
    }

    void CallEventCanvas()
    {
        eventCanvas.gameObject.SetActive(true);
        eventCanvas.CountDown();
        ChangeState(state, Phase.End);
    }
}
