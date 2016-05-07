using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class Lane : Target {
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

        UpdateLaneScore();
    }

    public int CardsCount(PlayerSide side)
    {
        return cards[(int)side].Count;
    }

    private MeshRenderer meshRenderer;
    private Color highlightColor;
    private List<Card>[] cards;

    private void UpdateLaneScore()
    {
        //throw new NotImplementedException();
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
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = new List<Card>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
