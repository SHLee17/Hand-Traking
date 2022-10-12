using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right
}
public enum PoseType
{
    Number
}
public class Pose : MonoBehaviour
{
    public Direction dir;
    public RSPGameManager.RSP rsp;
    public int num;
    public bool select;

    public void SelectFinger(bool b) => select = b; 

}
