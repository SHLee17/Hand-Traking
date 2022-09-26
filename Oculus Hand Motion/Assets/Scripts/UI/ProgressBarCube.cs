using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarCube : MonoBehaviour
{
    [SerializeField]
    public MeshRenderer barRenderer;
    void Start()
    {
        barRenderer = GetComponent<MeshRenderer>();
        barRenderer.material.color = Color.blue;
    }


    
}
