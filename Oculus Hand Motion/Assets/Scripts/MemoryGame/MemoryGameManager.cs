using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour
{

    //static MemoryGameManager instance;

    //public static MemoryGameManager Instance
    //{
    //    get
    //    {
    //        if (ReferenceEquals(instance, null))
    //            return null;

    //        return instance;
    //    }
    //}
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

    [System.Serializable]
    public struct Matrix
    {
        public int i, j;
        public Matrix(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }

    public int gameLevel;


    [Header("Arrays")]
    [SerializeField]
    public List<Matrix> playerClickList;
    [SerializeField]
    List<Pad> clickHistroyList;
    [SerializeField]
    List<Matrix> exampleClickList;
    [SerializeField]
    List<PadButton> padButtonList;
    [SerializeField]
    GameObject[] objPanel;

    int count;
    float timer;
    float timerReset;
    State state;
    Phase phase;

    //private void Awake()
    //{
    //    if (ReferenceEquals(instance, null))
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //        Destroy(gameObject);
    //}

    void Start()
    {
        playerClickList = new List<Matrix>();
        exampleClickList = new List<Matrix>();
        gameLevel = 3;
        timer = timerReset = 1.5f;
        count = 5;

        SetLevel();

        state = State.PlayExample;
        phase = Phase.Ready;

    }
    void Update()
    {


        timer -= Time.deltaTime;

        Vector3 cameraPos = GameManager.Instance.player.cameraRig.centerEyeAnchor.transform.position + 
            new Vector3(-0.1f, -0.2f, 0.65f);

        transform.position = cameraPos;

        switch (state)
        {
            case State.PlayExample:
                switch (phase)
                {
                    case Phase.Ready:
                        //PadInteractle(false);
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

                                exampleClickList.Add(new Matrix(randLine, randButton));

                                timer = timerReset;
                                count -= 1;
                            }
                        }
                        else
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
    public void PadOnClick()
    {
  

        if (state == State.PlayGame && phase == Phase.Start)
        {
            //int i = temp / gameLevel;
            //int j = temp % gameLevel;
            ////playerClickStack.Push(new Matrix(i, j));
            //clickHistroyList[temp].txtNumber.text = temp.ToString();
        }
    }
    public void RemoveOnClick()
    {

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
