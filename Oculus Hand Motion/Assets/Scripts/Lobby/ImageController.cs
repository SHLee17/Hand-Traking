using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    public Image thisImage;

    public void SetImage(Sprite image)
    {
        thisImage.sprite = image;
    }
}
