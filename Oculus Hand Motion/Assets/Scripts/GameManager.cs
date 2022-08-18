using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TMP_Text debug;
    public GameObject obj;

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand));

    }
}
