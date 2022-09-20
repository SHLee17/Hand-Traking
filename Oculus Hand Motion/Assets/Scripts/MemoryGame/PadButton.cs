using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class PadButton : MonoBehaviour
{
    public bool isClick;

    [SerializeField]
    int index;

    [SerializeField]
    TMP_Text txtIndex;

    [SerializeField]
    PokeInteractable poke;

    [SerializeField]
    Animator animator;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public int Index { get => index;
        set 
        {
            index = value;
            txtIndex.text = index.ToString();

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

}
