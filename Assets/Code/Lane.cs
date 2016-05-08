using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class Lane : Target {
    [Serializable]
    public class StrongpointContainer
    {
        public PlayerSide side;
        public Strongpoint[] points;
    }

    public GameplayState gameplayState;
    public LaneHexagon laneHexagon;
    public StrongpointContainer[] strongpoints;

    public override TargetType GetTargetType()
    {
        return TargetType.LANE;
    }

    public override void HideHighlight()
    {
        highlightColor.a = 0.0f;
        meshRenderer.material.color = highlightColor;
    }

    public override void HideHoverHighlight()
    {
        highlightColor.a = 0.5f;
        meshRenderer.material.color = highlightColor;
    }

    public override void ShowHighlight()
    {
        highlightColor.a = 0.5f;
        meshRenderer.material.color = highlightColor;
    }

    public override void ShowHoverHighlight()
    {
        highlightColor.a = 1f;
        meshRenderer.material.color = highlightColor;
    }

    public void AddCard(Card card, PlayerSide side)
    {
        cards[(int)side].Add(card);

        UpdateCardPositions(side);

        UpdateLanePower();

        if(battleTurn <= 0)
        {
            //battle
            battleTurn = turnsToBattle;
            Battle();
        }
    }

    public int CardsCount(PlayerSide side)
    {
        return cards[(int)side].Count;
    }

    private MeshRenderer meshRenderer;
    private Color highlightColor;
    private List<Card>[] cards;
    private int[] powerLevel;
    private int battleTurn = turnsToBattle;
    private const int turnsToBattle = 4;

    private void Battle()
    {
        int friendly = (int)PlayerSide.Friendly, enemy = (int)PlayerSide.Enemy;
        //compare power levels

        //when equal, destroy one of each
        if (powerLevel[friendly] == powerLevel[enemy])
        {
            Destroy(cards[friendly][0].gameObject);
            cards[friendly].RemoveAt(0);

            Destroy(cards[enemy][0].gameObject);
            cards[enemy].RemoveAt(0);
        }
        //when not equal, destroy one for each unit difference
        else if (powerLevel[friendly] > powerLevel[enemy]) 
        {
            DestroyStrongpoint(friendly, enemy);
            DestroyCards(friendly, enemy);
        }
        else
        {
            DestroyStrongpoint(enemy, friendly);
            DestroyCards(enemy, friendly);
        }

        UpdateCardPositions(PlayerSide.Friendly);
        UpdateCardPositions(PlayerSide.Enemy);
    }

    private void DestroyStrongpoint(int winner, int looser)
    {
        int cardsToDestroy = Mathf.CeilToInt(powerLevel[winner] - powerLevel[looser]);
        if (cardsToDestroy > cards[looser].Count)
        {
            bool found = false;
            //destroy strongpoint
            for (int i = 0; i < strongpoints.Length; i++)
            {
                if((int)strongpoints[i].side == looser)
                {
                    for (int j = 0; j < strongpoints[i].points.Length; j++)
                    {
                        if(strongpoints[i].points[j].enabled == true)
                        {
                            found = true;
                            strongpoints[i].points[j].DestroyStrongpoint();
                            gameplayState.TurretDestroyed(looser);
                            break;
                        }
                    }
                    break;
                }
            }

            if (found == false)
            {
                //end game, nexus destroyed
                gameplayState.EndGame(winner);
            }
        }
    }

    private void DestroyCards(int winner, int looser)
    {
        int cardsToDestroy = Mathf.CeilToInt(powerLevel[winner] - powerLevel[looser]);
        cardsToDestroy = Mathf.Clamp(cardsToDestroy, 0, cards[looser].Count);
        for (int i = 0; i < cardsToDestroy; i++)
        {
            Destroy(cards[looser][0].gameObject);
            cards[looser].RemoveAt(0);
        }
    }

    private void UpdateLanePower()
    {
        //temporary
        powerLevel[(int)PlayerSide.Friendly] = cards[(int)PlayerSide.Friendly].Count;
        powerLevel[(int)PlayerSide.Enemy] = cards[(int)PlayerSide.Enemy].Count;

        laneHexagon.UpdatePowerSlider(powerLevel[(int)PlayerSide.Friendly], powerLevel[(int)PlayerSide.Enemy]);
        battleTurn--;
        if (battleTurn <= 0)
        {
            laneHexagon.ResetTurn();
        }
        else
        {
            laneHexagon.SetTurn(battleTurn);
        }
    }

    private void UpdateCardPositions(PlayerSide side)
    {
        Transform nexusTransform = side == PlayerSide.Enemy ? GameWorld.Instance.EnemyNexus.transform : GameWorld.Instance.FriendlyNexus.transform;
        Vector3 dir = nexusTransform.position - transform.position;
        dir.Normalize();

        for (int i = 0; i < cards[(int)side].Count; i++)
        {
            cards[(int)side][i].transform.DOMove(transform.position + dir * (i + 1) * 0.4f, 1);
        }
    }

    // Use this for initialization
    void Awake () {
        meshRenderer = GetComponent<MeshRenderer>();
        highlightColor = meshRenderer.material.color;
        cards = new List<Card>[(int)PlayerSide.Count];
        powerLevel = new int[(int)PlayerSide.Count];
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = new List<Card>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
