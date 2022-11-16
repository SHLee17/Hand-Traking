using UnityEngine;
using UnityEngine.UI;
using System;


public class CardSpriteList : MonoBehaviour
{
    public Sprite[] sprites;
    public Image myImage;

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
    }

    public void CardSetter(int cardNum)
    {
        myImage.sprite = sprites[cardNum];
    }
}
