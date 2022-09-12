using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventCanvas : MonoBehaviour
{
    [SerializeField]
    TMP_Text txtWording;
    public bool isEventOver;

    string[] countDown = 
        {"1 2 3 4 <color=red>5</color>",
                    "1 2 3 <color=orange>4</color>",
                     "1 2 <color=yellow>3</color>",
                    "1 <color=green>2</color>",
                    "<color=blue>1</color>"};

    
    private void OnEnable()
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

            count++;
        }

        yield return wf;
        isEventOver = true;
        gameObject.SetActive(false);
    }
}
