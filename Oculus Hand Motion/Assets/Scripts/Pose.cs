using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right
}
public enum Finger
{
    Thumb,
    Index,
    Middle,
    Ring,
    Pinky
}
public class Pose : MonoBehaviour
{
    public Direction dir;
    public Finger finger;
    public bool select;

    public void SelectFinger(bool b) => select = b; 

}
