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

        ((Lane)Caller.Target).AddCard(card, card.Owner.Side);
    }
}
