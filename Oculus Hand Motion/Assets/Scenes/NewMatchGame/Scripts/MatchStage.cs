using System.Collections.Generic;
using UnityEngine;

 
public class MatchStage : MonoBehaviour
{
    public enum Type
    {
        Rectangle,
        Triangle
    }
    public Type type;
    public int invenCount;
    public List<Blank> inventoryList;
    public List<Angle> stage;
}
