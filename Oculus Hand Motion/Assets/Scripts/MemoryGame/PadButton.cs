using UnityEngine;
using TMPro;
using Oculus.Interaction;
using MemoryGame;

public class PadButton : MonoBehaviour
{
    public bool isClick;
    private PadColor padColor;

    [SerializeField]
    int index;


    [SerializeField]
    PokeInteractable poke;

    [SerializeField]
    Animator animator;

    string buttonKey;

    public InteractableDebugVisual colorVisual;
    public TMP_Text txtName;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public int Index { 
        get => index;
        set 
        {
            txtName.fontSize = 80;
            index = value;
            //txtIndex.text = index.ToString();
        }  
    }

    public PadColor PadColor { 
        get => padColor;
        set 
        {
            padColor = value;
            txtName.fontSize = 25;
            Color color = Color.white;
            switch (value)
            {
                case PadColor.Red:
                    color = Color.red;
                    break;
                case PadColor.Blue:
                    color = Color.blue;
                    break;
                case PadColor.Green:
                    color = Color.green;
                    break;
                case PadColor.Yellow:
                    color = new Color(254f / 255f, 190f / 255f, 0, 1);
                    break;
                case PadColor.Orange:
                    color = new Color(255f / 255f, 127f / 255f, 0, 1);
                    break;
                case PadColor.Purple:
                    color = new Color(128f / 255f, 0, 128f / 255f, 1);
                    break;
                case PadColor.White:
                    color = Color.white;
                    break;
            }
            colorVisual.NormalColor = color;
            colorVisual.DisabledColor = color;
            //txtIndex.text = value.ToString();
        }
    }

    public string ButtonKey { 
        get => buttonKey; 
        set 
        {
            buttonKey = value;
        }
    }

    public void InteractableActive(bool isActive) => poke.enabled = isActive;
    public void Click(float speed)
    {
        animator.SetTrigger("Click");
        animator.speed = speed;
    }

    public void OnClick(bool isClick) => this.isClick = isClick;

    //public void ColorChange(Color color) => colorVisual.NormalColor = color;

}
