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


    public List<Blank> blankList;
    static public List<Blank> inventoryList;
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

        blankList.ForEach(x => x.type = Blank.Type.Blank);
        inventoryList.ForEach(x => x.type = Blank.Type.Inven);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
