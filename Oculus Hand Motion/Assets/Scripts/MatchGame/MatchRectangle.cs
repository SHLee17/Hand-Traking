using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MatchRectangle : MonoBehaviour
{

    [SerializeField]
    List<Blank> blankList;
    [SerializeField]
    Match tempMatch;
    [SerializeField]
    List<Blank> inventoryPosList;

    private void Start()
    {
        foreach (Transform parent in transform)
        {
            foreach (Transform child in parent)
                blankList.Add(child.GetComponent<Blank>());
        }

        //foreach (Blank item in blankList)
        //{
        //    if (item.isMatchActive) 
        //        item.match = Instantiate(tempMatch, 
        //            item.transform.position, 
        //            quaternion.identity, 
        //            transform);
        //}
    }
}
