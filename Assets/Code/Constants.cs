using UnityEngine;
using System.Collections;

public static class Constants {
    public const string apiKey = "ef6817e2-1924-4de0-9056-e694fe202a19";
    public const string region = "eune";

    public const string championRestUrl = "https://global.api.pvp.net/api/lol/static-data/{0}/v1.2/champion?champData=image,tags&api_key={1}";
    public const string versionsRestUrl = "https://global.api.pvp.net/api/lol/static-data/{0}/v1.2/versions?api_key={1}";

    public const string mapImageUrl = "http://ddragon.leagueoflegends.com/cdn/{0}/img/map/map11.png";
    public const string championMediumImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/{0}_{1}.jpg";
    public const string championSmallImageUrl = "http://ddragon.leagueoflegends.com/cdn/{0}/img/champion/{1}.png";
}
