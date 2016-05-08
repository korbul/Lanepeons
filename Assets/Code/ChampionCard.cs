using UnityEngine;
using DG.Tweening;

public class ChampionCard : Card {
    public Texture2D thumb;
    public ChampionDto championData;

    public void Shrink()
    {
        transform.DOScale(smallSize, 0.5f);
        meshRenderer.material.mainTexture = thumb;
    }

    public void Enlarge()
    {
        transform.DOScale(cardSize, 0.5f);
        meshRenderer.material.mainTexture = image;
    }

    private readonly Vector3 smallSize = new Vector3(0.4f, 0.4f, 1);
}
