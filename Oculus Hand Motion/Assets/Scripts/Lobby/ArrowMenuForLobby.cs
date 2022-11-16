using Oculus.Interaction;
using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowMenuForLobby : MonoBehaviour
{
    public LobbyManager manager;

    [SerializeField]
    InteractableDebugVisual upArrow;
    [SerializeField]
    InteractableDebugVisual downArrow;

    public void Plus()
    {
        manager.selectedMenu++;
    }
    public void Minus()
    {
        manager.selectedMenu--;
    }
}
