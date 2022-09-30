using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public bool isActive;
    Match match = null;
    [SerializeField]
    MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();

            if (match != null)
            {
                match.objCurrentBlank = gameObject;
                isActive = true;
                meshRenderer.material.color = new Color(1,0,0,0.3f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();

            if (match != null)
            {
                match.objCurrentBlank = gameObject;
                isActive = true;
                meshRenderer.material.color = new Color(1, 0, 0, 0.3f);
            }
        }
    }
    private void Update()
    {
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            Match match = other.GetComponent<Match>();

            if (match != null)
            {
                if (match.isSelect)
                {
                    meshRenderer.material.color = new Color(1,1,1,0.3f);

                    match.objCurrentBlank = null;
                    isActive = false;
                }
            }
        }
    }

}
