using UnityEngine;

public class Match : MonoBehaviour
{
    public int index;
    public bool isSelect;
    public Blank currentBlank;
    public Blank nextMoveBlank;
    public MatchStage currentStage;

    public void Selected(bool isSelect)
    {
        this.isSelect = isSelect;

        if (!isSelect)
        {
            if (nextMoveBlank != null)
            {
                if (currentBlank != nextMoveBlank)
                {
                    Swap(currentBlank, nextMoveBlank);

                    nextMoveBlank = null;
                    PosChange(currentBlank.transform);
                    return;
                }
            }
            foreach (Blank item in currentStage.inventoryList)
            {
                if (item.match == null && item.gameObject.activeSelf)
                {
                    Swap(currentBlank, item);
                    PosChange(currentBlank.transform);
                    return;
                }
            }

            PosChange(currentBlank.transform);
        }

    }

    public void Swap(Blank current, Blank target)
    {
        //current.isMatchActive = false;
        current.match = null;

        //target.isMatchActive = true;
        target.match = this;

        currentBlank = target;
    }

    public void PosChange(Transform transform)
    {
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }
}
