using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class LobbyPos : MonoBehaviour
{
    Vector3 cameraOffset;
    Quaternion cameraRotation;

    void Start()
    {
        cameraOffset = new Vector3(-0.2f, 0, 0.4f);
        cameraRotation = new Quaternion( 0, 0, 0, 0);
        GameManager.Instance.ResetTimer(gameObject, cameraOffset, cameraRotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
