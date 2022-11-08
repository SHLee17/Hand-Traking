using System.Collections.Generic;
using UnityEngine;

public class MatchStage1 : MonoBehaviour
{
    [SerializeField]
    Transform inventoryParent;
    [SerializeField]
    Transform blankParent;
    [SerializeField]
    int invenCount;

    public GameObject objInfo;
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
