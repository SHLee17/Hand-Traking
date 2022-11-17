using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public List<GameObject> ses = new List<GameObject>();

    public void PlaySE(int seNum)
    {
        GameObject se = Instantiate(ses[seNum], Vector3.zero, Quaternion.identity);
        Destroy(se,4f);
    }
}
