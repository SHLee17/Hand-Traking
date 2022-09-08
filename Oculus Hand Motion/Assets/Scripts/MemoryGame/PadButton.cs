using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;

public class PadButton : MonoBehaviour
{
    [SerializeField]
    int index;
    [SerializeField]
    TMP_Text txtIndex;

    [SerializeField]
    PokeInteractable poke;

    [SerializeField]
    MeshRenderer cube;

    [SerializeField]
    Animator animator;

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
        //debugVisual.enabled = isActive;
    }
    public void Click()
    {
        animator.SetTrigger("Click");
        
    }

    public void OnClick()
    {
        //int temp = index - 1;
        //int i = temp / MemoryGameManager.Instance.gameLevel;
        //int j = temp % MemoryGameManager.Instance.gameLevel;
        //MemoryGameManager.Instance.playerClickList.Add(new MemoryGameManager.Matrix(i, j));

        //Debug.Log(MemoryGameManager.Instance.playerClickList.Count);

    }

}
