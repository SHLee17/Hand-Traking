using UnityEngine;

public class Blank : MonoBehaviour
{
    public enum ActiveMatch
    {
        deactive,
        active
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


    Match tempMatch;
    public Match match;
    public ActiveMatch activeMatch;
    public Answer answer;
    [SerializeField]
    MeshRenderer meshRenderer;


    Color ActiveColor;
    Color DeactiveClolor;

    private void Start()
    {
        ActiveColor = new Color(0, 1, 0, 0.3f);
        DeactiveClolor = new Color(1, 1, 1, 0.3f);
        meshRenderer.material.color = DeactiveClolor;
        match = null;
    }

    private void Update()
    {
        if (match != null)
        {
            meshRenderer.enabled = false;

            if (match.isSelect)
            {
                meshRenderer.material.color = ActiveColor;
                meshRenderer.enabled = true;
            }
            else
            {
                if (match.currentBlank != null)
                {
                    if(match.currentBlank == this)
                    {
                        if (!match.isSelect)
                        {
                            match.PosChange(match.currentBlank.transform);

                            return;
                        }
                    }
                    else
                    {
                        if (!match.isSelect)
                        {
                            match.Unselected();
                            if (match.currentBlank != this) match = null;

                            return;
                        }
                    }
                }
            }
        }
        else
        {
            meshRenderer.material.color = DeactiveClolor;
            meshRenderer.enabled = true;
        }

        if (tempMatch != null)
        {
            if (!tempMatch.isSelect)
            {
                if (tempMatch.nextMoveBlank == null)
                {
                    foreach (Blank item in tempMatch.currentStage.inventoryList)
                    {
                        if (item.gameObject.activeSelf)
                        {
                            if (item.match == null)
                            {
                                item.match = tempMatch;
                                tempMatch.Swap(tempMatch.currentBlank, target: item);
                                tempMatch.PosChange(tempMatch.currentBlank.transform);
                                tempMatch.currentBlank = this;
                                tempMatch = null;
                                return;
                            }
                        }
                    }
                    tempMatch.PosChange(tempMatch.currentBlank.transform);
                }

                tempMatch = null;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();
            tempMatch = null;
            match.nextMoveBlank = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Match"))
        {
            match = other.GetComponent<Match>();
            match.nextMoveBlank = null;
            tempMatch = match;
            match = null;
        }

    }
}
