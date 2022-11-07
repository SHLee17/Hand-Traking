using Unity.VisualScripting;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public enum ActiveMatch
    {
        active,
        deactive
    }
    public enum Answer
    {   
        None,
        Unconditionally,
        Alpha,
        Beta,
        Gamma,
        Delta
    }
    //public bool isMatchActive;
    //public bool isRightAnswer;

    public Match match = null;
    public ActiveMatch activeMatch;
    public Answer answer;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    BoxCollider boxCollider;

    Color ActiveColor;
    Color DeactiveClolor;

    private void Start()
    {
        ActiveColor = new Color(0, 1, 0, 0.3f);
        DeactiveClolor = new Color(1, 1, 1, 0.3f);
        meshRenderer.material.color = DeactiveClolor;
        boxCollider = GetComponent<BoxCollider>();


    }

    private void Update()
    {
        if (match != null)
        {
            meshRenderer.material.color = ActiveColor;

            //if (isMatchActive)
            //{
                meshRenderer.enabled = false;
                boxCollider.enabled = false;
            //}
        }
        else
        {
            meshRenderer.material.color = DeactiveClolor;
            meshRenderer.enabled = true;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();

            if (match != null /*&& !isMatchActive*/)
            {
                if (match.isSelect)
                {
                    match.nextMoveBlank = this;
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Match"))
            match = null;

    }
}
