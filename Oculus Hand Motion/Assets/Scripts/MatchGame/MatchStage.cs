using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchStage : MonoBehaviour
{
    [SerializeField]
    Transform inventoryParent;
    [SerializeField]
    Transform blankParent;
    [SerializeField]
    int invenCount;

    public List<Blank> blankList;
    public List<Blank> inventoryList;
    void Start()
    {
        foreach (Transform parent in blankParent)
        {
            foreach (Transform child in parent)
                blankList.Add(child.GetComponent<Blank>());
        }

        inventoryList = new List<Blank>();

        foreach (Transform item in inventoryParent)
            inventoryList.Add(item.GetComponent<Blank>());

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (invenCount <= i)
                inventoryList[i].gameObject.SetActive(false);
        }


    }

}
