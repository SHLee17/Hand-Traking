using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCardManager : MonoBehaviour
{
    public SubManager subManager;
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

    public Transform objBoard;

    void Start()
    {
        rand = new System.Random();
        level = 1;
        picked = false;
        correctCards = 0;
        subManager.correctNum = 0;
        subManager.clearBonus = 0;
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
        
    }

    void Update()
    {
        transform.position =
            new Vector3(transform.position.x, GameManager.Instance.player.cameraRig.centerEyeAnchor.position.y - .2f, transform.position.z);
        //transform.LookAt(GameManager.Instance.player.cameraRig.transform);

        int temp = cardsCount;
        level = arrow.Value;
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
            if (i >= level)
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

    public void FlipThatCard(CardManager thatCard, bool setCard)
    {
        thatCard.FlipCard(setCard);
    }

    public void Judge()
    {
        correctCards += 2;
        subManager.seManager.PlaySE(1);
        subManager.correctNum = correctCards/2;
        if (correctCards == cardsCount)
        {
            subManager.clearBonus = 1000;
            subManager.levelControl.clearStage = true;
            CompleteGame();
        }
    }

    public void CompleteGame()
    {
        subManager.levelControl.CompleteGame();
    }

}