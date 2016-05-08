using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    public ActionChain championActionChain;

    public Card GetChampionCardById(int id)
    {
        ChampionDto champion = null;

        foreach(ChampionDto cdto in GameWorld.Instance.Champions.Data)
        {
            if(cdto.Id == id)
            {
                champion = cdto;
                break;
            }
        }

        if (null == champion)
            return null;

        GameObject cardObject = new GameObject();
        cardObject.name = champion.Name;

        GameObject cardArtObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        cardArtObject.name = "Card Mesh";

        //a mesh collider is created by CreatePrimitive(). We don't need it
        Destroy(cardArtObject.GetComponent<MeshCollider>());

        cardArtObject.transform.SetParent(cardObject.transform, false);

        cardArtObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Texture"));

        ChampionCard card = cardObject.AddComponent<ChampionCard>();
        card.championData = champion;
        card.cardMesh = cardArtObject;
        card.onPlayActionChain = championActionChain;
        card.cardName = champion.Name;

        BoxCollider boxCollider = cardObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1, 1, 0.1f);

        Texture2D texture;
        if (championMediumTextures.TryGetValue(champion.Id, out texture))
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

        card.Init();
        return card;
    }

    public Card GetRandomChampionCard()
    {
        ChampionDto champion = GameWorld.Instance.Champions.Data[Random.Range(0,GameWorld.Instance.Champions.Data.Length)];
        return GetChampionCardById(champion.Id);
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
