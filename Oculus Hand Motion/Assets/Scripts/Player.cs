using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public HandGrabInteractor[] handGrabInteractors;
    //public GameObject


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnButton()
    {
        GameManager.Instance.OnOption();
    }
}
