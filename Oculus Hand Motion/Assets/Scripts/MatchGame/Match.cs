using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public int index;
    public bool isSelect;
    public GameObject objInteractable;

    

    public void Selected(bool isSelect)
    {
        this.isSelect = isSelect;
    }
}
