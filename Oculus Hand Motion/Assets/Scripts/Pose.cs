using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right
}

public class Pose : MonoBehaviour
{
    public Direction dir;
    public int num;
    public bool select;

    public void SelectFinger(bool b) => select = b; 

}
