using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRManager ovrManager;
    public OVRCameraRig cameraRig;
    public HandGrabInteractor[] handGrabInteractors;

    public void OnButton()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        Vector3 gmPos = gm.transform.position;
        Quaternion gmRot = gm.transform.rotation;
        Vector3 selPos = cameraRig.transform.position;
        Quaternion selRot = cameraRig.transform.rotation;

        selPos = gmPos;
        selRot = gmRot;
        
        //cameraRig.transform.position = new Vector3(, pos.y, pos.z - 0.5f);
    }
}
