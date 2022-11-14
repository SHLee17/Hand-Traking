using System.Collections.Generic;
using TMPro;
using UnityEngine;

 
public class MatchStage : MonoBehaviour
{
    public enum Type
    {
        Rectangle,
        Triangle
    }
    public Type type;
    //public int invenCount;
    public List<Blank> inventoryList;
    public List<Angle> stage;
    public GameObject objHint;
    public TMP_Text txtHint;

    public void hintActive(bool isActive) => objHint.gameObject.SetActive(isActive);
}
