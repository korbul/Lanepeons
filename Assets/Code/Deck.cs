using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public ActionChain championActionChain;

    public Card GetRandomChampionCard()
    {
        ChampionDto champion = GameWorld.Instance.Champions.Data[Random.Range(0,GameWorld.Instance.Champions.Data.Length)];

        GameObject cardObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        cardObject.name = champion.Name;

        cardObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Texture"));

        ChampionCard card = cardObject.AddComponent<ChampionCard>();
        card.onPlayActionChain = championActionChain;
        card.cardName = champion.Name;

        Texture2D texture;
        if(championMediumTextures.TryGetValue(champion.Id, out texture))
        {
            card.image = texture;
            card.thumb = championSmallTextures[champion.Id];
        }
        else
        {
            texture = new Texture2D(8, 8);
            texture.name = string.Format("{0}_image", champion.Name);
            championMediumTextures[champion.Id] = texture;
            card.image = texture;

            StartCoroutine(DownloadImage(string.Format(Constants.championMediumImageUrl, champion.Key, "0"), texture));

            texture = new Texture2D(8, 8);
            texture.name = string.Format("{0}_thumb", champion.Name);
            championSmallTextures[champion.Id] = texture;
            card.thumb = texture;

            StartCoroutine(DownloadImage(string.Format(Constants.championSmallImageUrl, GameWorld.Instance.DragonDataVersion, champion.Key), texture));
        }

        return card;
    }

    private Dictionary<int, Texture2D> championMediumTextures = new Dictionary<int, Texture2D>();
    private Dictionary<int, Texture2D> championSmallTextures = new Dictionary<int, Texture2D>();

    private static IEnumerator DownloadImage(string url, Texture2D texture)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("failed for " + www.url);
        }
        else
        {
            texture.Resize(www.texture.width, www.texture.height);
            texture.SetPixels32(www.texture.GetPixels32());
            texture.Apply();
        }
    }
}
