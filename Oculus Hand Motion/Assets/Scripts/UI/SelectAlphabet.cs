using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAlphabet : MonoBehaviour
{
    [SerializeField]
    List<GameObject> alphabets;
    [SerializeField]
    int index;

    public void UpButton()
    {
        index++;

        if (index >= alphabets.Count)
            index = 0;

        for (int i = 0; i < alphabets.Count; i++)
        {
            if (i != index)
                alphabets[i].gameObject.SetActive(false);
        }

        alphabets[index].SetActive(true);
    }
    public void DownButton()
    {
        index--;
        if (index < 0)
            index = alphabets.Count - 1;

        for (int i = 0; i < alphabets.Count; i++)
        {
            if (i != index)
                alphabets[i].gameObject.SetActive(false);
        }

        alphabets[index].SetActive(true);
    }
}
