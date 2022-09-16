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
    [SerializeField]
    GameObject objOptionSelect;
    private void Start()
    {

        GameManager.Instance.gameObject.SetActive(true);
        ResetObject();
    }
    public void ResetObject()
    {
        objFinalComplete.gameObject.SetActive(false);
        objOptionSelect.gameObject.SetActive(true);
    }

    public void Completed()
    {
        objFinalComplete.SetActive(true);
        objOptionSelect.SetActive(false);
    }

    public void FinalCompleted(bool isBool)
    {
        if (!isBool)
        {
            objFinalComplete.SetActive(false);
            objOptionSelect.SetActive(true);
        }
        else
            isOptionCompleted = true;
    }
    
}
