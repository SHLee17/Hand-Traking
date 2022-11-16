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

    private void Start()
    {
        txtWording.fontSize = 40;
        txtWording.text = "잠시후 게임이 시작 됩니다.";
    }

    public void CountDown() => StartCoroutine(StartCount());
    public void EndEvent() => StartCoroutine(CanvasEndEvent());
    public void StartGame() => StartCoroutine(CanvasStartGame());
    IEnumerator StartCount()
    {
        isEventOver = false;
        txtWording.fontSize = 40;
        txtWording.text = "<rainb>모두 기억 하셨나요?</rainb>";
        yield return new WaitForSeconds(3);
        WaitForSeconds wfs = new WaitForSeconds(1);

        for (int i = 3; i > 0; i--)
        {
            txtWording.text = $"<fade>{i}</fade>";
            txtWording.fontSize = 100;
            yield return wfs;
        }

        isEventOver = true;
        txtWording.fontSize = 40;
        gameObject.SetActive(false);
        
    }

    IEnumerator CanvasEndEvent()
    {
        isEventOver = false;
        yield return new WaitForSeconds(3);
        isEventOver = true;
        gameObject.SetActive(false);
    }

    IEnumerator CanvasStartGame()
    {
        isEventOver = false;

        WaitForSeconds wfs = new WaitForSeconds(1);
        WaitForSeconds wfs2 = new WaitForSeconds(3);


        yield return wfs2;

        txtWording.fontSize = 40;
        txtWording.text = "<bounce>잠시후 게임이 시작 됩니다.</bounce>";
        yield return wfs2;


        for (int i = 3; i > 0; i--)
        {
            txtWording.text = $"<fade>{i}</fade>";
            txtWording.fontSize = 100;
            yield return wfs;
        }
        gameObject.SetActive(false);
        isEventOver = true;
    }
}
