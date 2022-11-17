using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public SubManager subManager;
    public List<GameObject> fireWorks;
    public bool clearStage = false;

    public void CompleteGame()
    {
        StartCoroutine("FireWorkOn");
    }

    IEnumerator FireWorkOn()
    {
        if (clearStage)
        {
            yield return new WaitForSeconds(2f);
            foreach (var item in fireWorks)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(1f);
            }
            subManager.seManager.PlaySE(3);
            yield return new WaitForSeconds(4f);
        }
        
        subManager.ShowResult();
    }
}
