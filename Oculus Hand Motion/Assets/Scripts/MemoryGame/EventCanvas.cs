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

    string[] countDown = 
        {"1 2 3 4 <size=150><color=red>5</color></size>",
                    "1 2 3 <size=150><color=orange>4</color></size> 5",
                     "1 2 <size=150><color=yellow>3</color></size> 4 5",
                    "1 <size=150><color=green>2</color></size> 3 4 5",
                    "<size=150><color=blue>1</color></size> 2 3 4 5"};

    public void CountDown()
    {
        isEventOver = false;
        StartCoroutine(StartCount());
    }

    IEnumerator StartCount()
    {
        yield return new WaitForSeconds(3);
        WaitForSeconds wf = new WaitForSeconds(1);
        int count = 0;
        while (count < countDown.Length)
        {
            yield return wf;
            txtWording.text = countDown[count];
            txtWording.fontSize = 100;

            count++;
        }

        yield return wf;
        isEventOver = true;
        txtWording.fontSize = 50;
        gameObject.SetActive(false);
    }
}
