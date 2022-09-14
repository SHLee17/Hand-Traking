using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumGameOption : MonoBehaviour
{
    public ArrowMenu levelSelect;
    public ArrowMenu exampleMaxCount;
    public bool isOptionCompleted;
    [SerializeField]
    GameObject objFinalComplete;



    public void Completed()
    {
        objFinalComplete.SetActive(true);
    }

    public void FinalCompleted(bool isBool)
    {
        if (!isBool)
            objFinalComplete.SetActive(false);
        else
        isOptionCompleted = true;
    }
    
}
