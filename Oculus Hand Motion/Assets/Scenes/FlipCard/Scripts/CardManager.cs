using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public FlipCardManager flipCardManager;
    public GameObject thisCard;
    public Animator flipCardAnim;
    public CardSpriteList thisCardImage;
    public int cardPos;
    public int cardNum;

    bool checkPair;
    

    private void Start()
    {
        flipCardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FlipCardManager>();

        checkPair = false;
    }

    public void PickTheCard()
    {
        if (GetStatus())
            return;

        if (flipCardManager.processing)
            return;

        FlipCard(true);

        if (flipCardManager.picked)
        {
            checkPair = flipCardManager.pickedCard.cardNum == cardNum;
            if (checkPair)
            {
                flipCardManager.Judge();
            }
            else
            {
                flipCardManager.subManager.seManager.PlaySE(6);
                StartCoroutine(WaitAndFlip(1.5f));
            }

            flipCardManager.picked = false;
        }
        else
        {
            flipCardManager.pickedCard = this;
            flipCardManager.picked = true;
        }

        StartCoroutine(progressing(1.5f));
    }

    IEnumerator WaitAndFlip(float i)
    {
        yield return new WaitForSeconds(i);
        FlipCard(false);
        flipCardManager.FlipThatCard(flipCardManager.pickedCard, false);
    }

    IEnumerator progressing(float i)
    {
        flipCardManager.processing = true;
        yield return new WaitForSeconds(i);
        flipCardManager.processing = false;
    }

    public void FlipCard(bool setCard)
    {
        flipCardAnim.SetBool("Selected", setCard);
    }

    bool GetStatus()
    {
        return flipCardAnim.GetBool("Selected");
    }
}
