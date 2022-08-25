using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance 
    {
        get
        {
            if (ReferenceEquals(instance, null))
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (ReferenceEquals(instance, null))
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand));
        //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand));
    }






}
