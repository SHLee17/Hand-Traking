using System.Collections.Generic;
using UnityEngine;

public class NewMatchStage : MonoBehaviour
{
    public SpawnMatches inventoryParent;
    public SpawnMatches blankGaroParent;
    public SpawnMatches blankSeroParent;
    [SerializeField]
    int invenCount;

    public GameObject objInfo;
    public List<Blank> blankList;
    public List<Blank> inventoryList;
    void Start()
    {
        foreach (Transform parent in blankGaroParent.gameObject.transform)
        {
            foreach (Transform child in parent)
                blankList.Add(child.GetComponent<Blank>());
        }

        inventoryList = new List<Blank>();

        foreach (Transform item in inventoryParent.gameObject.transform)
            inventoryList.Add(item.GetComponent<Blank>());

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (invenCount <= i)
                inventoryList[i].gameObject.SetActive(false);
        }


    }

}
