using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SubManager : MonoBehaviour
{
    public GameManager gameManager;
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
        terrains = new List<GameObject>();
        foreach (Transform terrain in gameManager.transform)
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
        resultCalcText = string.Format("{0:00}��\n{0:00.0}��\n{0:0000}��\n", correctNum, scoreMultiple, clearBonus);
        resultNum = (int)(correctNum * scoreMultiple) + clearBonus;
        resultText = string.Format("{0:0000}��\n", resultNum);

        resultCalc.text = resultCalcText;
        result.text = resultText;

        resultPlate.SetActive(true);
    }

    //public void 
}
