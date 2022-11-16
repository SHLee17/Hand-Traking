using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRManager ovrManager;
    public OVRCameraRig cameraRig;
    public HandGrabInteractor[] handGrabInteractors;

    public void OnButton()
    {
        GameManager.Instance.OnOption();
    }
}
