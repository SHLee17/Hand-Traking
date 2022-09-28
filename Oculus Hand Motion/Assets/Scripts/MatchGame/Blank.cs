using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public bool isActive;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            Match match = other.GetComponent<Match>();

            if (match != null)
            {
                if (!match.isSelect)
                {
                    //match.transform.SetParent(transform);
                    match.transform.localPosition = transform.localPosition;
                    match.transform.localRotation = transform.localRotation;
                    Debug.Log("Match");
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Match"))
        //{
        //    Match match = other.GetComponent<Match>();

        //    if (match != null)
        //    {
        //        match.transform.position = transform.position;
        //        match.transform.rotation = Quaternion.identity;
        //    }
        //}
    }
}
