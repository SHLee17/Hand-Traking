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

        //if (!isSelect)
        //{
        //    if (nextMoveBlank != null)
        //    {
        //        if (currentBlank != nextMoveBlank)
        //        {
        //            Swap(currentBlank, nextMoveBlank);

        //            nextMoveBlank = null;
        //            PosChange(currentBlank.transform);
        //            return;
        //        }
        //    }
        //    foreach (Blank item in currentStage.inventoryList)
        //    {
        //        if (item.gameObject.activeSelf) {
        //            if (item.match == null)
        //            {
        //                Swap(currentBlank, item);
        //                PosChange(currentBlank.transform);
        //                return;
        //            }
        //        }
        //    }

        //    PosChange(currentBlank.transform);
        //}

    }
  
    public void Unselected()
    {
        if (nextMoveBlank != null)
        {
            if (currentBlank != nextMoveBlank)
            {
                Swap(currentBlank, nextMoveBlank);
                PosChange(currentBlank.transform);
                return;
            }
        }
        //foreach (Blank item in currentStage.inventoryList)
        //{
        //    if (item.gameObject.activeSelf)
        //    {
        //        if (item.match == null)
        //        {
        //            Swap(currentBlank, item);
        //            PosChange(currentBlank.transform);
        //            return;
        //        }
        //    }
        //}

        
    }

    public void Swap(Blank current, Blank target)
    {
        current.match = null;
        target.match = this;
        currentBlank = target;
    }

    public void PosChange(Transform transform)
    {
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }
}
