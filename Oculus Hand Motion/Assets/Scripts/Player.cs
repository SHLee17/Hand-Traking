using Oculus.Interaction.HandGrab;
using UnityEngine;

public class Player : MonoBehaviour
{
    public OVRCameraRig cameraRig;
    public HandGrabInteractor[] handGrabInteractors;

    public void OnButton()
    {
        GameManager.Instance.OnOption();
    }
}
