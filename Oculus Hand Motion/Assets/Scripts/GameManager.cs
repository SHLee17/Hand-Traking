using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public List<Pose> poses;
    int count;
    public bool fingerCheck;

    void Start()
    {
        fingerCheck = false;
    }

    void Update()
    {

        if (fingerCheck)
        {
            foreach (var item in poses)
            {
                if (item.select)
                    count += 1;
            }

            Debug.Log(count);
            count = 0;
        }

        //Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand));
    }




}
