using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MemoryGame;
using Unity.VisualScripting;

public class Pad : MonoBehaviour
{
    public PadColor color;
    public TMP_Text txtName;
    public Image image;
    public Sprite[] sprites;

    [SerializeField]
    Shadow shadow;

    [SerializeField]
    Image btnImage;
}
