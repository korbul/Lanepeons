using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class ChampionLanePlay : IBehaviour {
    public void Execute(Action Caller)
    {
        ChampionCard card = (ChampionCard)Caller.CallerCard;

        //resize card
        card.Shrink();

        Lane targetLane = GameWorld.Instance.Lanes.Find(x => x.name == Caller.Target);
        if(null != targetLane)
            targetLane.AddCard(card, card.Owner.Side);
    }
}
