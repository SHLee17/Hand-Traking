using UnityEngine;
using TMPro;
using Oculus.Interaction;
using MemoryGame;

public class PadButton : MonoBehaviour
{
    public bool isClick;
    private PadColor color;

    [SerializeField]
    int index;

    [SerializeField]
    TMP_Text txtIndex;

    [SerializeField]
    PokeInteractable poke;

    [SerializeField]
    Animator animator;

    public InteractableDebugVisual colorVisual;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public int Index { 
        get => index;
        set 
        {
            txtIndex.fontSize = 80;
            index = value;
            txtIndex.text = index.ToString();
        }  
    }

    public PadColor PadColor { 
        get => color;
        set 
        { 
            color = value;
            txtIndex.fontSize = 25;

            switch (value)
            {
                case PadColor.Red:
                    colorVisual.NormalColor = Color.red;
                    colorVisual.DisabledColor = Color.red;
                    txtIndex.text = value.ToString();
                    break;

                case PadColor.Blue:
                    colorVisual.NormalColor = Color.blue;
                    colorVisual.DisabledColor = Color.blue;
                    txtIndex.text = value.ToString();
                    break;

                case PadColor.Green:
                    colorVisual.NormalColor = Color.green;
                    colorVisual.DisabledColor = Color.green;
                    txtIndex.text = value.ToString();
                    break;

                case PadColor.Yellow:
                    Color color = new Color(254f / 255f, 190f / 255f, 0, 1);
                    colorVisual.NormalColor = color;
                    colorVisual.DisabledColor = color;
                    txtIndex.text = value.ToString();
                    break;

                case PadColor.Orange:
                     color = new Color(205f / 255f, 127 / 255f, 50 / 255f, 1);
                    colorVisual.NormalColor = color;
                    colorVisual.DisabledColor = color;
                    txtIndex.text = value.ToString();
                    break;

                case PadColor.Purple:
                    color = new Color(128f / 255f, 0, 128f / 255f, 1);
                    colorVisual.NormalColor = color;
                    colorVisual.DisabledColor = color;
                    txtIndex.text = value.ToString();
                    break;
            }

        }
    }

    public void InteractableActive(bool isActive)
    {
        poke.enabled = isActive;
    }
    public void Click(float speed)
    {
        animator.SetTrigger("Click");
        animator.speed = speed;
    }

    public void OnClick(bool isClick) => this.isClick = isClick;

    //public void ColorChange(Color color) => colorVisual.NormalColor = color;

}
