using UnityEngine;

public class Match : MonoBehaviour
{
    public int index;
    public bool isSelect;
    public Blank currentBlank;
    public Blank nextMoveBlank;

    public void Selected(bool isSelect)
    {
        this.isSelect = isSelect;
        Debug.Log(isSelect);
        if (!isSelect)
        {
            if (nextMoveBlank != null)
            {
                if (currentBlank != nextMoveBlank)
                {
                    currentBlank.isMatchActive = false;
                    currentBlank.match = null;
                    currentBlank = nextMoveBlank;
                    currentBlank.isMatchActive = true;
                    currentBlank.match = this;
                    nextMoveBlank = null;
                    PosChange(currentBlank.transform);
                    return;
                }
            }
                foreach (Blank item in MatchStage.inventoryList)
                {
                    if (item.match == null)
                    {
                        currentBlank.isMatchActive = false;
                        currentBlank.match = null;
                        currentBlank = item;
                        currentBlank.isMatchActive = true;
                        currentBlank.match = this;
                        PosChange(currentBlank.transform);
                        return;
                    }
                }
            
            PosChange(currentBlank.transform);
        }

    }
    public void Hover()
    {
        Debug.Log("hover");
    }

    private void Update()
    {
        //if (!isSelect)
        //    GameManager.Instance.player.grabInteractor.Select();
    }

    public void PosChange(Transform transform)
    {
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }
}
