using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TMP_Text debug;
    public GameObject obj;

    void Start()
    {
        debug.text = obj.name;
    }

    void Update()
    {
        
    }
}
