using Unity.VisualScripting;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public enum Pair
    {
        None,
        Unconditionally,
        One,
        Two,
        Three,
        Four
    }
    public bool isMatchActive;
    public bool isRightAnswer;
    public Match match = null;
    public Pair pair;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    BoxCollider boxCollider;

    

    private void Start()
    {
        meshRenderer.material.color = new Color(1, 1, 1, 0.3f);
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (match != null)
        {
            meshRenderer.material.color = new Color(0, 1, 0, 0.3f);

            if (isMatchActive)
            {
                meshRenderer.enabled = false;
                boxCollider.enabled = false;
            }
        }
        else
        {
            meshRenderer.material.color = new Color(1, 1, 1, 0.3f);
            meshRenderer.enabled = true;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();

            if (match != null && !isMatchActive)
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
