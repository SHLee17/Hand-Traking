using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MemoryGame;

public class Pad : MonoBehaviour
{
    public PadColor color;
    public TMP_Text txtName;

    [SerializeField]
    Shadow shadow;

    [SerializeField]
    Image btnImage;
}
