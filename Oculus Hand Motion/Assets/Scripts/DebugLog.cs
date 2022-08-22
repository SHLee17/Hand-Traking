using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    void Start()
    {
        Debug.Log(gameObject.name);
    }

}
