using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{
    enum State
    {
        GameStart,
        Judgment
    }
    enum Phase
    {
        Ready,
        Start,
        End
    }
    public List<MatchNumber> matchNumberList;
    void Start()
    {
        
    }

    void Update()
    {

    }
}
