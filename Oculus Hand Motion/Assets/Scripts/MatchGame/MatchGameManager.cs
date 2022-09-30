using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{

    public List<Blank> blankList;
    public List<MatchNumber> matchNumberList;

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
    void Start()
    {
        
    }

    void Update()
    {

    }
}
