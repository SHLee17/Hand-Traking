using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public enum Game
    {
        Num,
        RSP,
        Match,
        Memory,
        Card,
        Total
    }

    public class User
    {
        public string name;
        public Dictionary<Game, int> score;

        public User()
        {
            name = "AAA";
            score = new Dictionary<Game, int>();
            score.Add(Game.Num, 0);
            score.Add(Game.RSP, 0);
            score.Add(Game.Match, 0);
            score.Add(Game.Memory, 0);
            score.Add(Game.Card, 0);
            score.Add(Game.Total, 0);

        }
    }

    public User currentUser;

    public void CreateUser()
    {
        currentUser = new User();
    }

}
