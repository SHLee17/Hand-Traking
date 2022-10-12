using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorEvenet : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public void SetColorRed() => meshRenderer.material.color = Color.red;
    public void SetColorWhite() => meshRenderer.material.color = Color.white;
}
