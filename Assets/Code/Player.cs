using UnityEngine;
using System.Collections;

public enum PlayerSide
{
    Friendly = 0,
    Enemy = 1,
    Neutral = 2,
    Count = 3
}

public class Player {
    static int index = 0;

    int id;
    string name;
    PlayerSide side;

    public Player(string name, PlayerSide side)
    {
        Id = index;
        index++;
        Name = name;
        Side = side;
    }

    public int Id
    {
        get
        {
            return id;
        }

        private set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        private set
        {
            name = value;
        }
    }

    public PlayerSide Side
    {
        get
        {
            return side;
        }

        private set
        {
            side = value;
        }
    }
}
