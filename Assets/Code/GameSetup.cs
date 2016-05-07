using UnityEngine;
using System.Collections;
using System;

public class GameSetup : MonoBehaviour {

    public GameObject map;

    private string championRestUrl;
    private string versionsRestUrl;
    private string mapImageUrl;

    // Use this for initialization
    IEnumerator Start () {
        yield return StartCoroutine(SetupApiData());
        yield return StartCoroutine(SetupMap());
        yield return StartCoroutine(SetupChampions());
        yield return StartCoroutine(SetupPlayers());
    }

    private IEnumerator SetupApiData()
    {
        championRestUrl = string.Format(Constants.championRestUrl, Constants.region, Constants.apiKey);
        versionsRestUrl = string.Format(Constants.versionsRestUrl, Constants.region, Constants.apiKey);

        Debug.Log("requesting " + versionsRestUrl);
        WWW www = new WWW(versionsRestUrl);
        yield return www;
        string[] versions = JsonHelper.getJsonArray<string>(www.text);
        if (versions.Length > 0)
            GameWorld.Instance.DragonDataVersion = versions[0];

        mapImageUrl = string.Format(Constants.mapImageUrl, GameWorld.Instance.DragonDataVersion);
    }

    private IEnumerator SetupMap()
    {
        Debug.Log("requesting " + mapImageUrl);
        WWW www = new WWW(mapImageUrl);
        yield return www;
        Renderer mapRenderer = map.GetComponent<Renderer>();
        mapRenderer.material.mainTexture = www.texture;
    }

    private IEnumerator SetupChampions()
    {
        Debug.Log("requesting " + championRestUrl);
        WWW www = new WWW(championRestUrl);
        yield return www;
        //ChampionListDto champions = JsonUtility.FromJson<ChampionListDto>(www.text);
        var N = SimpleJSON.JSON.Parse(www.text);

        ChampionListDto champions = new ChampionListDto();
        champions.Type = N["type"];
        champions.Version = N["version"];

        SimpleJSON.JSONNode data = N["data"];
        champions.Data = new ChampionDto[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            champions.Data[i] = new ChampionDto();
            champions.Data[i].Id = data[i]["id"].AsInt;
            champions.Data[i].Key = data[i]["key"];
            champions.Data[i].Name = data[i]["name"];
            champions.Data[i].Title = data[i]["title"];
            champions.Data[i].Tags = new string[data[i]["tags"].Count];
            for (int j = 0; j < data[i]["tags"].Count; j++)
            {
                champions.Data[i].Tags[j] = data[i]["tags"][j];
            }
        }

        GameWorld.Instance.Champions = champions;
    }

    private IEnumerator SetupPlayers()
    {
        GameWorld.Instance.FriendlyPlayer = new Player("Gigel", PlayerSide.Friendly);
        GameWorld.Instance.EnemyPlayer = new Player("Ionel", PlayerSide.Enemy);

        yield return null;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
