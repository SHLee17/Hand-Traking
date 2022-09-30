using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    public int index;
    public bool isSelect;
    [SerializeField]
    public GameObject objCurrentBlank;
    [SerializeField]
    public Transform startPos;
    

    public void Selected(bool isSelect)
    {
        this.isSelect = isSelect;
    }


    private void Update()
    {
        if (!isSelect)
        {
            if (objCurrentBlank != null)
            {
                transform.position = objCurrentBlank.transform.position;
                transform.rotation = objCurrentBlank.transform.rotation;
            }
            else if (objCurrentBlank == null)
            {
                transform.position = startPos.position;
                transform.rotation = startPos.rotation;
            }
        }
    }


}
