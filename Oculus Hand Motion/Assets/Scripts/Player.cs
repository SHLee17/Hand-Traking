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
        Vector3 pos = gm.transform.position;
        Vector3 self = cameraRig.transform.position;
        
        //cameraRig.transform.position = new Vector3(, pos.y, pos.z - 0.5f);
    }
}
