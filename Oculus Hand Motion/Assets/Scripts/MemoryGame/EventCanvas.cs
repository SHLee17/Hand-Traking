using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventCanvas : MonoBehaviour
{
    [SerializeField]
    public TMP_Text txtWording;
    public bool isEventOver;

    public void CountDown()
    {
        isEventOver = false;
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        txtWording.text = "모두 기억 하셨나요?";
        yield return new WaitForSeconds(3);
        WaitForSeconds wf = new WaitForSeconds(1);

        for (int i = 5; i > 0; i--)
        {
            txtWording.text = i.ToString();
            txtWording.fontSize = 100;
            yield return wf;
        }

        isEventOver = true;
        txtWording.fontSize = 50;
        gameObject.SetActive(false);
        
    }
}
