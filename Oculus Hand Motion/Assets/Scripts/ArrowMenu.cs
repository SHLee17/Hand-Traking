using Oculus.Interaction;
using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowMenu : MonoBehaviour
{
    int value;

    public int max;
    public int min;

    [SerializeField]
    InteractableDebugVisual upArrow;
    [SerializeField]
    InteractableDebugVisual downArrow;
    [SerializeField]
    TMP_Text txtValue;

    public int Value
    {
        get { return value; }
        set 
        { 
            this.value = value;
            if(txtValue != null)
            txtValue.text = value.ToString();
        }
    }


    private void Start()
    {
        Value = min;
    }

    public void Plus()
    {
        if(max > Value)
        Value += 1;
    }
    public void Minus()
    {
        if (Value > min)
            Value -= 1;
    }


}
