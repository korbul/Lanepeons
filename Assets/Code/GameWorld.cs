using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameWorld : MonoBehaviour {

    public static GameWorld Instance
    {
        get
        {
            return instance;
        }
    }

    public Player FriendlyPlayer { get; set; }
    public Player EnemyPlayer { get; set; }

    public ChampionListDto Champions { get; set; }

    public List<Lane> Lanes
    {
        get
        {
            return lanes;
        }
    }

    public GameObject FriendlyNexus
    {
        get
        {
            return friendlyNexus;
        }
    }

    public GameObject EnemyNexus
    {
        get
        {
            return enemyNexus;
        }
    }

    public string DragonDataVersion { get; set; }

    [SerializeField]
    private List<Lane> lanes;
    [SerializeField]
    private GameObject friendlyNexus;
    [SerializeField]
    private GameObject enemyNexus;

    private static GameWorld instance;

    void Awake()
    {
        instance = this;
    }
}
