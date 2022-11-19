using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class FlipCardManager : MonoBehaviour
{
    public SubManager subManager;
    public ProgressBar progressBar;
    public GameObject[] levels;
    public CardManager[] cards;
    public List<CardManager> cardList;
    public int level;
    private int[] cardsCountByLevel = { 8, 12, 18, 24, 32, 40, 50 };
    public int cardsCount;
    int[] orders;
    System.Random rand;
    public ArrowMenu arrow;
    public bool picked;
    public CardManager pickedCard;
    int correctCards;
    public bool processing;

    public float timer;
    float timerReset;

    public Transform objBoard;
    public bool isStart;

    [SerializeField]
    GameObject objHolder;
    [SerializeField]
    Vector3 cameraOffset;
    Quaternion cameraRotation;

    void Start()
    {
        //timer setting
        progressBar.StartProtress();
        timer = timerReset = 60f;
        progressBar.gameObject.SetActive(true);
        progressBar.Set(timer, timerReset);
        //
        subManager.correctNum = 0;
        subManager.clearBonus = 0;
        subManager.gameObject.SetActive(false);
        rand = new System.Random();
        level = 1;
        picked = false;
        correctCards = 0;
        processing = false;
        cardList = new List<CardManager>();

        foreach (Transform child in objBoard)
        {
            if (child.childCount > 0)
            {
                foreach (Transform item in child)
                {
                    CardManager temp = item.GetComponent<CardManager>();
                    if (temp != null)
                        cardList.Add(temp);
                }
            }
        }
        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].cardPos = i;
        }

        cameraOffset = new Vector3(-0.2f, 0.2f, 0.4f);
        cameraRotation = new Quaternion(0, 0, 0, 0);
        GameManager.Instance.ResetTimer(gameObject, cameraOffset, cameraRotation);
    }

    void Update()
    {
        if (!isStart)
        {
            return;
        }
        //timer
        timer -= Time.deltaTime;
        progressBar.Set(timer, timerReset);

        if (timer < 0)
        {
            //gameObject.SetActive(false);
            objHolder.gameObject.SetActive(false);
            subManager.gameObject.SetActive(true);
            subManager.ShowResult();
        }
        //
        int temp = cardsCount;
        level = 3;
        cardsCount = cardsCountByLevel[level - 1];
        if (temp != cardsCount)
        {
            SetLevel();
        }
    }

    public void LevelControl(int selectedLevel)
    {
        level = selectedLevel;
        SetLevel();
    }

    public void SetLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(true);
            if (i>=level)
                levels[i].SetActive(false);
        }

        int orderCount = cardsCount - 1;
        orders = new int[cardsCount];
        for (int i = 0; i <= orderCount; i++)
        {
            int temp = rand.Next(0, orderCount - i);
            for (int j = 0; j <= temp; j++)
            {
                if (orders[j] != 0)
                    temp++;
            }
            orders[temp] = i + 1;
        }

        for (int i = 0; i < cardsCount; i++)
        {
            int cardNum = (orders[i] - 1) / 2;
            cardList[i].FlipCard(false);
            cardList[i].thisCardImage.CardSetter(cardNum);
            cardList[i].cardNum = cardNum;
        }

    }

    public void StartGame()
    {
        isStart = true;
    }

    public void FlipThatCard(CardManager thatCard, bool setCard)
    {
        thatCard.FlipCard(setCard);
    }

    public void Judge()
    {
        correctCards += 2;
        subManager.seManager.PlaySE(1);
        subManager.correctNum = correctCards / 2;
        if (correctCards == cardsCount)
        {
            objHolder.gameObject.SetActive(false);
            subManager.gameObject.SetActive(true);
            isStart = false;
            subManager.levelControl.clearStage = true;
            subManager.clearBonus = 1000;
            subManager.levelControl.CompleteGame();
        }
    }
}
