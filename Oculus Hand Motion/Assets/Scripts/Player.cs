using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRCameraRig cameraRig;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
