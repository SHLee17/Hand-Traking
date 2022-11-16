using Oculus.Interaction.HandGrab;
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
