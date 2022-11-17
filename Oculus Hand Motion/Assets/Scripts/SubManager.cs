using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SubManager : MonoBehaviour
{
    public List<GameObject> terrains;
    public CompleteLevel levelControl;
    public MusicManager musicBox;
    public SEManager seManager;
    public GameObject resultPlate;
    public TextMeshProUGUI resultCalc;
    public TextMeshProUGUI result;
    public int mapNum = 0;
    public int correctNum = 0;
    public float scoreMultiple = 0.0f;
    public int clearBonus = 0;
    public int resultNum;
    string resultCalcText;
    string resultText;

    private void Start()
    {
        //musicBox.PlayMusic();
        terrains = new List<GameObject>();
        foreach (Transform terrain in GameManager.Instance.transform)
        {
            GameObject terra = terrain.gameObject;
            terrains.Add(terra);
        }
        foreach (var item in terrains)
        {
            item.SetActive(false);
        }
        terrains[mapNum].SetActive(true);
    }

    public void ShowResult()
    {
        resultCalcText = string.Format("{0:00}개\n{1:00.0}배\n{2:0000}점\n", correctNum, scoreMultiple, clearBonus);
        resultNum = (int)(correctNum * scoreMultiple) + clearBonus;
        resultText = string.Format("{0:0000}점\n", resultNum);

        resultCalc.text = resultCalcText;
        result.text = resultText;

        resultPlate.SetActive(true);
        //GameManager.Instance.AddTotal(resultNum);
    }

    public void NextScene(int sceneNum)
    {
        GameManager.Instance.NextScene(sceneNum);
    }
}
